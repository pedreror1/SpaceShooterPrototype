using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMG : MonoBehaviour
{
    private List<Enemy> currentEnemies = new List<Enemy>();
    private Transform[] Asteroids;
    [SerializeField] [Tooltip("Put Here The parent of Valid Waypoints")]
    private Transform AsteroidsMG;
    [SerializeField]
    private Enemy[] enemiesPresets;
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
        WaypointManager.instance.Reset();
        if(Respawning)
        {
            for (int i = 0; i < currentEnemies.Count; i++)
            {
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
                currentEnemies.Add(Instantiate(enemiesPresets[Random.Range(0, enemiesPresets.Length - 1)],getNewTarget().position, Quaternion.identity));
                currentEnemies[i].Reset(false);
            }
        }
    }
    public void GameOver()
    {
        for (int i = 0; i < currentEnemies.Count; i++)
        {
            Destroy(currentEnemies[i].gameObject);
            currentEnemies.RemoveAt(i);
        }
    }
     
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
        Asteroids = AsteroidsMG.GetComponentsInChildren<Transform>(); 
    }
}
