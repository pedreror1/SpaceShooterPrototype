using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0f, 100f)][SerializeField]
    private float speed = 1f;
    [Range(0f, 10000f)] [SerializeField]
    private float fovRadius = 1f;
    [Range(0f, 100f)][SerializeField]
    private float minDistToShoot = 1f;
    [SerializeField] SphereCollider radiusCollider;
    private float dist;
    private Transform target;
    private Rigidbody rb;
    private WaitForSeconds shoodDelay = new WaitForSeconds(2.50f);
    private int health = 100;
    private int value = 500;
    bool canBeDamaged = true;
    enum state
    {
        flying,
        chasing
    }
    private state currentState = state.flying;
    [SerializeField]
    private Transform shootPos;
    private bool canShoot = true;
    private Transform currentBullet1;
 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        radiusCollider.radius = fovRadius;
        target = EnemyMG.Instance.getNewTarget();

    }
    public void GetDamage(int damage, string Tag ="Enemy")
    {
        if (canBeDamaged)
        {
          
            health -= damage;
            if (health <= 0)
            {
                canBeDamaged = false;
                Instantiate(GameManager.Instance.ExplosionParticle, transform.position, Quaternion.identity);
                if (Tag == "Player")
                {
                    GameManager.Instance.AddScore(value);
                    Instantiate(GameManager.Instance.CoinPrefab, transform.position, Quaternion.identity);
                }
                gameObject.SetActive(false);
            }
        }
    }
    public void Reset(bool wasPaused,EnemySettings settings)
    {
        if (!wasPaused)
        {
            rb.velocity = Vector3.zero;
            health = settings.Health;

        }
        value = settings.value;
        shoodDelay = new WaitForSeconds(settings.bulletCoolDown);
        speed = settings.speed;
        target = EnemyMG.Instance.getNewTarget();
        minDistToShoot = settings.distToShoot;
        GetComponentInChildren<Renderer>().materials[1] = settings.shipColor;
        fovRadius = settings.FOVRadius;

        radiusCollider.radius = fovRadius;
        canShoot = true;
    }


    void Fly(float speedMultiplier=1f)
    {
        /*  Vector3 targetRot = target.position - transform.position;
          Quaternion destRot = Quaternion.Euler(targetRot);
          destRot.z = transform.rotation.eulerAngles.z;

          Quaternion nextrot = Quaternion.Slerp(transform.rotation, destRot, Time.deltaTime);
         */
         
        transform.LookAt(target);
        rb.velocity = transform.forward * speed*(!canShoot?3f:1f);
        dist = Vector3.Distance(transform.position, target.position);
    }
    void ChaseOtherShip()
    {
        if (dist > minDistToShoot)
        {
            rb.velocity *= 2f;

        }
        else
        {
            rb.velocity = Vector3.zero;
            if (canShoot)
            {
                
                currentBullet1 = PoolSystem.Instance.getFromPool().transform;
                currentBullet1.GetComponent<bullet>().bulletTag = "Enemy";
                currentBullet1.position = shootPos.position;
                currentBullet1.rotation = shootPos.rotation;
                canShoot = false;
                radiusCollider.enabled = false;
                StartCoroutine(shootCoolOff());
                currentState = state.flying;
                target = EnemyMG.Instance.getNewTarget();
            }
        }
    }

    void Update()
    {
        if (target)
        {
            Fly();
            if (currentState == state.flying)
            {
                if (target.tag == "Player" || target.tag == "Enemy")
                {
                    target= EnemyMG.Instance.getNewTarget();
                }
                if (dist < 30f)
                {
                    target =  EnemyMG.Instance.getNewTarget();
                }
            }
            else
            {
                ChaseOtherShip();
            }
        }
    }


    IEnumerator shootCoolOff()
    {
        yield return shoodDelay;
        canShoot = true;
        radiusCollider.enabled = true;
        radiusCollider.radius = fovRadius;
        int randomChasePlayer = Random.Range(-100, 100);
        if(randomChasePlayer<0)
        {
            target = EnemyMG.Instance.player;
            currentState = state.chasing;
            radiusCollider.radius = 0f;
            Debug.Log("AY TE VOY!");
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player" || col.transform.tag == "Enemy" && canShoot )
        {
            target = col.transform;
            currentState = state.chasing;
            radiusCollider.radius = 0f;

        }
    }
}