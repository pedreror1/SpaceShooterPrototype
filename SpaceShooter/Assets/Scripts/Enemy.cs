using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0f, 100f)]
    [SerializeField]
    float speed = 1f;
    [Range(0f, 10000f)]
    [SerializeField]
    float FOVRadius = 1f;
    [Range(0f, 100f)]
    [SerializeField]
    float minDistToShoot = 1f;
    float dist;
    [SerializeField] SphereCollider radiusCollider;
    Transform Target;
    Rigidbody rb;
    WaitForSeconds ShoodDelay = new WaitForSeconds(4.5f);
    bool inShootingRange = false;

    [SerializeField] Transform shootPos1;
     bool canshoot = true;
    Transform currentBullet1;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!Target)
        {
            if (radiusCollider.radius < FOVRadius)
                radiusCollider.radius += Time.deltaTime*10;
            rb.velocity = transform.forward * speed;
            rb.AddTorque(0, Time.deltaTime, 0);
        }
        else
        {
            dist = Vector3.Distance(transform.position, Target.position);

            transform.LookAt(Target);
            if (dist > minDistToShoot)
            {
                rb.velocity = transform.forward * speed;
                inShootingRange = false;
            }
            else
            {
                rb.velocity = Vector3.zero;
                inShootingRange = true;
                if(canshoot)
                {
                    canshoot = false;
                    StartCoroutine(Shoot());
                }
                    
                 
               
            }

        }
    }
    IEnumerator Shoot()
    {
        while (inShootingRange)
        {
            yield return ShoodDelay;
           // print("sadf");
            currentBullet1 = PoolSystem.Instance.getFromPool().transform;
            currentBullet1.position = shootPos1.position;
            currentBullet1.rotation = shootPos1.rotation;
            if (dist > minDistToShoot)
            {
                inShootingRange = false;
            }


        }
        canshoot = true;

    }
    private void OnTriggerEnter(Collider col)
    {    
        if (col.transform.tag == "Player" || col.transform.tag == "Enemy")
        {
            Target = col.transform;
        }
    }
}