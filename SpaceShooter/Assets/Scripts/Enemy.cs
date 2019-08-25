using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 10f)]
    public float speed = 1f;
    [SerializeField] Transform player;
    [SerializeField]
    [Range(0f, 10f)]
    public float fov = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void checkCollision()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hito;
       if(Physics.Raycast(ray,out hito,fov))
       {
            print(hito.transform.name);
       }
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        transform.position += transform.forward * speed;
        checkCollision();
    }
}
