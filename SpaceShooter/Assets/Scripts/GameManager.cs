using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    int Score = 9999;
    int Coins = 0;
    int enemiesKilled = 13;
    int TimeRemaining= 60;
    int lifes = 3;
    public static GameManager instance;
    [SerializeField] Text coinText;
    public void AddCoin(int numCoins=1)
    {
        Coins+= numCoins;
        coinText.text = Coins.ToString().PadLeft(2, '0');
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
