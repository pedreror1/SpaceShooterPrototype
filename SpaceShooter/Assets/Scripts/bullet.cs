using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] [Range(0f, 100f)]
    private float speed = 1f;
    [SerializeField] [Range(0f, 100f)]
    private float fov = 1f;
    private WaitForSeconds lifeSpan = new WaitForSeconds(3f);

    public string bulletTag="Enemy";

    //TODO REMOVE THIS
    private void checkCollision()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hito;
        if (Physics.Raycast(ray, out hito, fov))
        {
            print(hito.transform.name);
            if(hito.transform.tag == "Player" )
            {
                hito.transform.GetComponent<Player>().getDamage(10);
            }
            else if( hito.transform.tag == "Enemy")
            {
                hito.transform.GetComponent<Enemy>().GetDamage(50,bulletTag);
            }
        }
    }
    private void OnEnable()
    {
        StartCoroutine(destroy());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().getDamage(10);
        }
        else if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().GetDamage(50, bulletTag);
        }
    }
    void FixedUpdate()
    {
        transform.position += transform.forward * speed;
        transform.Rotate(0f, 0f, 10f);
        //checkCollision();
        
    }
    IEnumerator destroy()
    {
        yield return lifeSpan;
        PoolSystem.Instance.AddtoPool(gameObject);

    }
}
