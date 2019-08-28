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
    WaitForSeconds ShoodDelay = new WaitForSeconds(2.50f);
    int Health = 100;


    enum state
    {
        flying,
        chasing
    }
    state currentState = state.flying;
    [SerializeField] Transform shootPos1;
    bool canshoot = true;
    Transform currentBullet1;
    AudioSource audioController;
    void Awake()
    {
        audioController = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        radiusCollider.radius = FOVRadius;
        EnemyMG.Instance.getNewTarget();

    }
    public void getDamage(int damage, string Tag ="Enemy")
    {
        Health -= damage;
        if (Health <= 0)
        {
            Instantiate(GameManager.Instance.ExplosionParticle, transform.position, Quaternion.identity);
            if (Tag == "Player")
            {
                GameManager.Instance.AddScore(500);
                Instantiate(GameManager.Instance.CoinPrefab, transform.position, Quaternion.identity);
            }
            gameObject.SetActive(false);
        }
    }
    public void Reset(bool wasPaused)
    {
        if (!wasPaused)
        {
            rb.velocity = Vector3.zero;
            Health = 100;

        }
        Target = EnemyMG.Instance.getNewTarget();
        radiusCollider.radius = FOVRadius;
        canshoot = true;
    }

    void Update()
    {
        if (Target)
        {
            transform.LookAt(Target);
            rb.velocity = transform.forward * speed;
            dist = Vector3.Distance(transform.position, Target.position);
            if (currentState == state.flying)
            {
                if (dist < 30f)
                {
                    EnemyMG.Instance.getNewTarget();
                }

            }
            else
            {
                if (dist > minDistToShoot)
                {
                    rb.velocity = transform.forward * speed;
                }
                else
                {
                    if (canshoot)
                    {
                        currentBullet1 = PoolSystem.Instance.getFromPool().transform;
                        currentBullet1.position = shootPos1.position;
                        currentBullet1.rotation = shootPos1.rotation;
                        canshoot = false;
                        StartCoroutine(shootCoolOff());
                        currentState = state.flying;
                        EnemyMG.Instance.getNewTarget();
                    }
                }



            }
        }
    }


    IEnumerator shootCoolOff()
    {
        yield return ShoodDelay;
        canshoot = true;
        radiusCollider.radius = FOVRadius;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player" || col.transform.tag == "Enemy")
        {
            Target = col.transform;
            currentState = state.chasing;
            radiusCollider.radius = 0f;

        }
    }
}