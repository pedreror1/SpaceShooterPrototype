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

     
    private void OnEnable()
    {
        StartCoroutine(destroy());
    }
    private void OnTriggerEnter(Collider other)
    {
 
        if (other.tag == "Player")
        {
            other.transform.parent.parent.GetComponent<Player>().getDamage(10);
        }
        else if (other.tag == "Enemy")
        {
            other.GetComponentInParent<Enemy>().GetDamage(50, bulletTag);
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
