using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform objective;
    [SerializeField] [Range(0f, 100f)]
    public float speed = 1f;
    [SerializeField]
    private SphereCollider radiusCollider;
    private Vector3 startposition;
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Enemy")
        {
            objective = col.transform;
        }
    }
    void Start()
    {
        startposition = transform.position;
    }
    void Destroy()
    {
        Instantiate(GameManager.Instance.ExplosionParticle, transform.position, Quaternion.identity);
        Collider[] nearObjects = Physics.OverlapSphere(transform.position, radiusCollider.radius*2);
        foreach (Collider col in nearObjects)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.GetDamage(100, "Player");
            }
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (objective)
        {
            transform.LookAt(objective);
            if (Vector3.Distance(transform.position, objective.position) <= 25f)
            {
                Destroy();
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, startposition) >= 725f)
            {
                Destroy();
            }
            transform.position += transform.forward * speed;
        }
    }
}
