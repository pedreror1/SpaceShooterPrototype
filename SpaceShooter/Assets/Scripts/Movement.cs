using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    [Range(0f, 100f)]
    float mouseSpeed = 1f;
    [SerializeField] [Range(0f, 100f)]
    float movementSpeed=1f;
    [SerializeField]
    [Range(0f, 1000f)]
    float rotationSpeed = 1f;
    [SerializeField]
    float hairCrossPosZ = 15f;
    [SerializeField]
    Transform hairCross;
    [SerializeField]
    Transform SpaceShip;
    [SerializeField]
    Transform CameraTransform;
    public Vector3 mousePosition;
    public bool invertY = true;
    float yaw, pitch;
    [MinMaxSlider(0f, 1920)]
    [SerializeField] Vector2 cameraHorizontalRange;
    [MinMaxSlider(0f, 1080)]
    [SerializeField] Vector2 cameraVerticalRange;
    Vector2 cameraOffset = Vector2.zero;

    Rigidbody rb;
    void Start()
    {
        Cursor.visible = false;
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
            cameraOffset.x = -1f;
        }
        else if (Input.mousePosition.x > cameraHorizontalRange.y)
        {
            cameraOffset.x = 1f;
        }
        else
        {
            cameraOffset.x = 0f;
        }

        if (Input.mousePosition.y < cameraVerticalRange.x)
        {
            cameraOffset.y = -1f;
        }
        else if (Input.mousePosition.y > cameraVerticalRange.y)
        {
            cameraOffset.y = 1f;
        }
        else
        {
            cameraOffset.y = 0f;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocity = SpaceShip.forward * Input.GetAxis("Vertical") * movementSpeed + SpaceShip.right * Input.GetAxis("Horizontal") * movementSpeed; ;
       rb.velocity= velocity;
        mousePosition.x =Mathf.Lerp(mousePosition.x,getValidValue(cameraHorizontalRange,Input.mousePosition.x),Time.deltaTime*mouseSpeed);
        mousePosition.y = Mathf.Lerp(mousePosition.y, getValidValue(cameraVerticalRange, Input.mousePosition.y), Time.deltaTime * mouseSpeed);
        mousePosition.z = hairCrossPosZ;
        calculateCameraOffset();
        print(rb.velocity.magnitude);
        if (rb.velocity.magnitude > 0)
        {
            var mouseMovement = new Vector2(Input.GetAxis("Mouse X") + cameraOffset.x, Input.GetAxis("Mouse Y") + cameraOffset.y * (invertY ? 1 : -1));

            float currentpitch = pitch + mouseMovement.y;
            float currentyaw = yaw + mouseMovement.x;
            yaw = Mathf.Lerp(yaw, currentyaw, Time.deltaTime * rotationSpeed);
            pitch = Mathf.Lerp(pitch, currentpitch, Time.deltaTime * rotationSpeed);
            CameraTransform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
        hairCross.position = Camera.main.ScreenToWorldPoint(mousePosition);
        SpaceShip.LookAt(hairCross);


    }
}
