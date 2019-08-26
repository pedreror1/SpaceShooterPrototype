using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public bool canShootProjectiles = true;

    int Health;
    int Shield;
    enum GameState
    {
        MainMenu,
        HighScores,
        Game,
        Shop
    }
    public bool CanMove=false;
    public bool CanAttack=false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        //print(other.transform.name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
