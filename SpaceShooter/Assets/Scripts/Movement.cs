using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] [Range(0f, 100f)]
    private float mouseSpeed = 1f;
    [SerializeField] [Range(0f, 200f)]
    private float movementSpeed =1f;
    [SerializeField] [Range(0f, 1000f)]
    private float rotationSpeed = 1f;
    [SerializeField]
    private float hairCrossPosZ = 15f;
    [SerializeField]
    private Transform hairCross;
    [SerializeField]
    private Transform SpaceShip;
    [SerializeField]
    private Transform CameraTransform;

    private float yaw, pitch;
    [SerializeField] [MinMaxSlider(0f, 1920)]
    private Vector2 cameraHorizontalRange;
    [SerializeField] [MinMaxSlider(0f, 1080)]
    private Vector2 cameraVerticalRange;
    private Vector2 cameraOffset = Vector2.zero;

    private Rigidbody rb;

    public Vector3 mousePosition;
    public bool invertY = true;
    void Start()
    {
        cameraHorizontalRange.x = Screen.width / 2f - Screen.width / 3f;
        cameraHorizontalRange.y= Screen.width / 2f + Screen.width / 3f;
        cameraVerticalRange.x = Screen.height / 2f - Screen.height / 3f;
        cameraVerticalRange.y = Screen.height / 2f + Screen.height / 3f;
        rb = GetComponent<Rigidbody>();
    }
    float getValidValue(Vector2 range, float currentvalue)
    {
        return Mathf.Max(range.x, Mathf.Min(range.y, currentvalue));
    }
    void calculateCameraOffset()
    {
        if (Input.mousePosition.x < cameraHorizontalRange.x)
        {
            cameraOffset.x = -10f;
        }
        else if (Input.mousePosition.x > cameraHorizontalRange.y)
        {
            cameraOffset.x = 10f;
        }
        else
        {
            cameraOffset.x = 0f;
        }

        if (Input.mousePosition.y < cameraVerticalRange.x)
        {
            cameraOffset.y = -10f;
        }
        else if (Input.mousePosition.y > cameraVerticalRange.y)
        {
            cameraOffset.y = 10f;
        }
        else
        {
            cameraOffset.y = 0f;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player.Instance.CanMove)
        {
            Vector3 velocity = SpaceShip.forward * Input.GetAxis("Vertical") * movementSpeed + SpaceShip.right * Input.GetAxis("Horizontal") * movementSpeed; ;
            rb.velocity = velocity;
            mousePosition.x = Mathf.Lerp(mousePosition.x, getValidValue(cameraHorizontalRange, Input.mousePosition.x), Time.deltaTime * mouseSpeed);
            mousePosition.y = Mathf.Lerp(mousePosition.y, getValidValue(cameraVerticalRange, Input.mousePosition.y), Time.deltaTime * mouseSpeed);
            mousePosition.z = hairCrossPosZ;
            calculateCameraOffset();
             
            if (rb.velocity.magnitude > 0)
            {
                var mouseMovement = new Vector2(Input.GetAxis("Mouse X") + cameraOffset.x, Input.GetAxis("Mouse Y") + cameraOffset.y * (invertY ? 1 : -1));

                float currentpitch = pitch + mouseMovement.y;
                float currentyaw = yaw + mouseMovement.x;
                yaw = Mathf.Lerp(yaw, currentyaw, Time.deltaTime * rotationSpeed);
                pitch = Mathf.Lerp(pitch, currentpitch, Time.deltaTime * rotationSpeed);
                CameraTransform.eulerAngles = new Vector3(pitch, yaw, 0f);
             //   rb.AddTorque(new Vector3(currentpitch, yaw, 0f));
                 
            }
            hairCross.position = Camera.main.ScreenToWorldPoint(mousePosition);
            SpaceShip.LookAt(hairCross);
        }

    }
}
