using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject GameUI, MainMenuUI;
    int Score = 9999;
    int Coins = 0;
    int enemiesKilled = 13;
    int TimeRemaining= 60;
    int lifes = 3;
    public static GameManager instance;
    [SerializeField] Text coinText, timeText;
    WaitForSeconds timerCycleLapse = new WaitForSeconds(1f);
   public enum GameState
    {
        MainMenu=0,
        Game=1,
        Shop=2,
        HighScores=3
    }
    public GameState currentState = GameState.MainMenu;
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
    IEnumerator GameTime()
    {
        while (TimeRemaining>0)
        {
            yield return timerCycleLapse;
            TimeRemaining--;
            timeText.text = TimeRemaining.ToString().PadLeft(2, '0');

        }
    }
    public void changeState(int newState)
    {
        currentState = (GameState)newState;
        switch(currentState)
        {
            case GameState.Game:
                StartCoroutine(GameTime());
                MainMenuUI.SetActive(false);
                GameUI.SetActive(true);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
