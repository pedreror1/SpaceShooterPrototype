using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField]
    private Transform shootPos1,shootPos2,haircross,spaceShip;
    [SerializeField]
    private Projectile projectilePrefab;
    private bool canshoot = true;
    private bool canshootProjectile = true;
    private Transform currentBullet1, currentBullet2;
    private Vector3 haircrossDirection;
    private WaitForSeconds gunCoolDownDelay = new WaitForSeconds(0.25f);
    private WaitForSeconds projectilesCoolDownDelay = new WaitForSeconds(3.5f);

    
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
    IEnumerator coolDownGuns()
    {
        yield return gunCoolDownDelay;
        canshoot = true;
    }
    IEnumerator coolDownRocketLauncher()
    {
        yield return projectilesCoolDownDelay;
        canshootProjectile = true;
    }
     void Update()
    {

        if (Player.Instance.CanAttack)
        {
            if (Input.GetMouseButtonDown(0) && canshoot)
            {
                shootBullets();
                canshoot = false;
                StartCoroutine(coolDownGuns());
            }
            if (Input.GetMouseButtonDown(1) && canshootProjectile && Player.Instance.Misiles > 0)
            {
                Player.Instance.Misiles--;
                canshootProjectile = false;
                shootProjectiles();
                StartCoroutine(coolDownRocketLauncher());
            }
        }
         
        
    }
}
