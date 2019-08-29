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
    private EnemySettings[] enemiesPresets;
    [SerializeField]
    private Enemy enemyPrefab;
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
                EnemySettings settings = enemiesPresets[Random.Range(0, enemiesPresets.Length - 1)];
                currentEnemies[i].Reset(true,settings);
            }
        }
        else
        {
            for (int i = 0; i < currentEnemies.Count; i++)
            {
                currentEnemies[i].gameObject.SetActive(true);
                EnemySettings settings = enemiesPresets[Random.Range(0, enemiesPresets.Length - 1)];
                currentEnemies[i].Reset(true, settings);
            }

            for (int i = 0; i < GameManager.Instance.settings.EnemiesPerRound; i++)
            {
                currentEnemies.Add(Instantiate(enemyPrefab,getNewTarget().position, Quaternion.identity));
                EnemySettings settings = enemiesPresets[Random.Range(0, enemiesPresets.Length - 1)];
                currentEnemies[i].Reset(true, settings);
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
