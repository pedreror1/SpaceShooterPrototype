using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int Health;
    int Shield;
    enum GameState
    {
        MainMenu,
        HighScores,
        Game,
        Shop
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.transform.name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
