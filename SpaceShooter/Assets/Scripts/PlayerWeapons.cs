using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField]
    Transform shootPos1,shootPos2,haircross,spaceShip;
     [SerializeField] Projectile projectilePrefab;
    bool canshoot = true;
    Transform currentBullet1, currentBullet2;
    Vector3 haircrossDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void shootBullets()
    {
        currentBullet1 = PoolSystem.Instance.getFromPool().transform;
        currentBullet1.position = shootPos1.position;
        currentBullet1.rotation = shootPos1.rotation;
        currentBullet2 = PoolSystem.Instance.getFromPool().transform;
        currentBullet2.position = shootPos2.position;
        currentBullet2.rotation = shootPos2.rotation;
        
        haircrossDirection = haircross.forward;
    }
    void shootProjectiles()
    {
         
                projectilePrefab.gameObject.SetActive(true);
          
         
    }
    // Update is called once per frame
    void Update()
    {
        if (Player.instance.CanAttack)
        {
            if (Input.GetMouseButtonDown(0) && Player.instance.CanAttack)
            {

                shootBullets();
               
                //canshoot = false;

            }
            if (Input.GetMouseButtonDown(1) && Player.instance.canShootProjectiles)
            {
                Player.instance.canShootProjectiles = false;
                shootProjectiles();
            }
        }
        
    }
}
