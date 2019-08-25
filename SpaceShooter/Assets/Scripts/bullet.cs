using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] [Range(0f, 100f)]
    public float speed = 1f;
    [SerializeField]
    [Range(0f, 10f)]
    public float fov = 1f;
    private void checkCollision()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hito;
        if (Physics.Raycast(ray, out hito, fov))
        {
            print(hito.transform.name);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.transform.name);
    }
    void FixedUpdate()
    {
        transform.position += transform.forward * speed;
        transform.Rotate(0f, 0f, 10f);
        checkCollision();
        if(Vector3.Distance(Player.instance.transform.position,transform.position)>1250f)
        {
            PoolSystem.Instance.AddtoPool(gameObject);
        }
    }
}
