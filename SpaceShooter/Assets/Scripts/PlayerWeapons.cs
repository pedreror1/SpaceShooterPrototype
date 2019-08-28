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
        GameManager.Instance.cameraShakeController.ShakeCamera(2.5f,0.53f);
        currentBullet1 = PoolSystem.Instance.getFromPool().transform;
        currentBullet1.position = shootPos1.position;
        currentBullet1.rotation = shootPos1.rotation;
        currentBullet2 = PoolSystem.Instance.getFromPool().transform;
        currentBullet2.position = shootPos2.position;
        currentBullet2.rotation = shootPos2.rotation;
        currentBullet1.GetComponent<bullet>().bulletTag = "Player";
        //currentBullet2.GetComponent<bullet>().bulletTag = "Player";
        haircrossDirection = haircross.forward;
    }
    void shootProjectiles()
    {
         
                projectilePrefab.gameObject.SetActive(true);
          
         
    }
    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.CanAttack)
        {
            if (Input.GetMouseButtonDown(0) && Player.Instance.CanAttack)
            {

                shootBullets();
               
                //canshoot = false;

            }
            if (Input.GetMouseButtonDown(1) && Player.Instance.canShootProjectiles)
            {
                Player.Instance.canShootProjectiles = false;
                shootProjectiles();
            }
        }
        
    }
}
