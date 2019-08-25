using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField]
    Transform shootPos1,shootPos2,haircross,spaceShip;
    List<bullet> bulletPool = new List<bullet>();
    public bullet bulletPrefab;
    bool canshoot = true;
    Transform currentBullet1, currentBullet2;
    Vector3 haircrossDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canshoot)
        {
            
            currentBullet1= Instantiate(bulletPrefab, shootPos1.position,shootPos1.rotation).transform;
            currentBullet2= Instantiate(bulletPrefab, shootPos2.position, shootPos2.rotation).transform;
            bulletPool.Add(currentBullet1.GetComponent<bullet>());
            bulletPool.Add(currentBullet2.GetComponent<bullet>());
            haircrossDirection = haircross.forward;
            //canshoot = false;

        }
        
    }
}
