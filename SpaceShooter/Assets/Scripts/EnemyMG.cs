using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMG : MonoBehaviour
{
    List<Enemy> currentEnemies = new List<Enemy>();
    Transform[] Asteroids;
    [SerializeField] Transform AsteroidsMG;
    [SerializeField]  Enemy[] enemiesPresets;
    public static EnemyMG Instance;

    public Transform getNewTarget()
    {
        return Asteroids[Random.Range(0, Asteroids.Length-1)];
    }
    public void Pause()
    {
        for (int i = 0; i < currentEnemies.Count; i++)
        {
            //on dead remove from here!
             currentEnemies[i].gameObject.SetActive(false);
        }
    }
    public void Reset(bool Respawning)
    {
        if(Respawning)
        {
            for (int i = 0; i < currentEnemies.Count; i++)
            {
                //on dead remove from here!
               
                currentEnemies[i].gameObject.SetActive(true);
                currentEnemies[i].Reset(true);
                
            }
        }
        else
        {
            for (int i = 0; i < currentEnemies.Count; i++)
            {
                currentEnemies[i].gameObject.SetActive(true);
                currentEnemies[i].Reset(false);
            }
                for (int i = 0; i < 20; i++)
            {
                currentEnemies.Add(Instantiate(enemiesPresets[Random.Range(0, enemiesPresets.Length - 1)], 
                    getNewTarget().position, Quaternion.identity));
                currentEnemies[i].Reset(false);

            }
        }
    }
    public void GameOver()
    {
        for (int i = 0; i < currentEnemies.Count; i++)
        {
            //on dead remove from here!

            Destroy(currentEnemies[i].gameObject);
            currentEnemies.RemoveAt(i);
           
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
        Asteroids = AsteroidsMG.GetComponentsInChildren<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
