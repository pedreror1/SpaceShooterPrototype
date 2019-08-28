using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] [Range(0f, 100f)]
    public float speed = 1f;
    [SerializeField]
    [Range(0f, 100f)]
    public float fov = 1f;
    WaitForSeconds lifeSpan = new WaitForSeconds(3f);
    public string bulletTag="Enemy";
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
                hito.transform.GetComponent<Enemy>().getDamage(50,bulletTag);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       // print(other.transform.name);
    }
    void FixedUpdate()
    {
        transform.position += transform.forward * speed;
        transform.Rotate(0f, 0f, 10f);
        checkCollision();
        StartCoroutine(destroy());
    }
    IEnumerator destroy()
    {
        yield return lifeSpan;
        PoolSystem.Instance.AddtoPool(gameObject);

    }
}
