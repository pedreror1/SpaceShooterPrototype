
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject HighscoreMainUI;
    [SerializeField] GameObject HighScoreSaveUI;
    [SerializeField] GameObject ShopUI;
    [SerializeField] GameObject EnemiesManager;
    [SerializeField] GameObject pauseUI;
    public GameObject ExplosionParticle;
    public CameraShake cameraShakeController;
    public int Score = 9999;
    int Coins = 0;
    int enemiesKilled = 13;
    int TimeRemaining = 60;
    int startTimer = 3;
    int lifes = 3;

    public static GameManager instance;
    [SerializeField] Text coinText, timeText, startTimerText;
    WaitForSeconds timerCycleLapse = new WaitForSeconds(1f);
    int level = 1;
    public AudioSource inGameMusicSource;
    public AudioSource UIMusicSource;
    public AudioSource EffectsSource;
    [SerializeField] AudioClip GameMusic;
    [SerializeField] AudioClip UIMusic;
    [SerializeField] AudioClip clickFX;
    [SerializeField] AudioClip countDownFX;
    [SerializeField] AudioClip startFX;
    [SerializeField] AudioClip welcomeFX;
    [SerializeField] AudioClip thanksFX;
    [SerializeField] AudioClip nextLevelFX;
    [SerializeField] AudioClip GameOverFX;
    public struct highScoreData
    {
        public string name;
        public int score;
        public highScoreData(string n, int s)
        {
            name = n;
            score = s;
        }
    }
    public List<highScoreData> HSData = new List<highScoreData>();
    public enum GameState
    {
        MainMenu = 0,
        Game = 1,
        Shop = 2,
        HighScorescreen = 3,
        HighScoreSave = 4,
        Pause = 5,

    }
    bool isPaused = false;
    public GameState currentState = GameState.MainMenu;
    public void AddCoin(int numCoins = 1)
    {
        Coins += numCoins;
        coinText.text = Coins.ToString().PadLeft(2, '0');
    }
    // Start is called before the first frame update
    void Start()
    {
        playMusic();
        instance = this;
        cameraShakeController = GetComponent<CameraShake>();
    }
    IEnumerator GameTime()
    {
        TimeRemaining = 60;
        startTimerText.color = Color.white;
        startTimer = 3;
        startTimerText.text = "3";
        startTimerText.GetComponent<Animator>().enabled = false;
        while (startTimer >= 0)
        {
            startTimer--;

            yield return timerCycleLapse;

            if (startTimer > 0)
            {
                startTimerText.text = startTimer.ToString();
            }
            else
            {
                startTimerText.text = "START!";
            }

        }
        Player.instance.CanMove = true;
        Player.instance.CanAttack = true;
        EnemiesManager.SetActive(true);
        startTimerText.GetComponent<Animator>().enabled = true;

        while (TimeRemaining > 0)
        {
            yield return timerCycleLapse;
            TimeRemaining--;
            timeText.text = TimeRemaining.ToString().PadLeft(2, '0');

        }
        Player.instance.CanMove = false;
        Player.instance.CanAttack = false;
        EnemiesManager.SetActive(false);
        level++;
        changeState(2);
    }
    public void playMusic(bool isGameMusic = false)
    {
        if (isGameMusic)
        {

            UIMusicSource.Pause();
            inGameMusicSource.Play();
        }
        else
        {
            UIMusicSource.UnPause();
            inGameMusicSource.Pause();
        }
    }
    public void playSoundFX(AudioClip clip)
    {
        EffectsSource.PlayOneShot(clip);
    }
    public void changeState(int newState)
    {
        currentState = (GameState)newState;
        switch (currentState)
        {
            case GameState.Game:
                playMusic(true);
                EnemiesManager.SetActive(true);
                ShopUI.SetActive(false);

                StartCoroutine(GameTime());
                MainMenuUI.SetActive(false);
                GameUI.SetActive(true);

                Cursor.visible = false;
                break;
            case GameState.HighScoreSave:
                playMusic();
                ShopUI.SetActive(false);

                GameUI.SetActive(false);
                Player.instance.CanMove = false;
                Player.instance.CanAttack = false;
                Cursor.visible = true;
                HighScoreSaveUI.SetActive(true);
                EnemiesManager.SetActive(false);
                break;
            case GameState.HighScorescreen:
                playMusic();
                ShopUI.SetActive(false);
                GameUI.SetActive(false);
                MainMenuUI.SetActive(false);

                HighScoreSaveUI.SetActive(false);
                HighscoreMainUI.SetActive(true);
                highscore.instance.LoadData();
                break;
            case GameState.MainMenu:
                playMusic();
                pauseUI.SetActive(false);
                ShopUI.SetActive(false);

                HighScoreSaveUI.SetActive(false);
                HighscoreMainUI.SetActive(false);
                MainMenuUI.SetActive(true);
                GameUI.SetActive(false);
                Player.instance.CanMove = false;
                Player.instance.CanAttack = false;
                Cursor.visible = true;

                break;
            case GameState.Shop:
                playMusic();
                ShopUI.SetActive(true);

                GameUI.SetActive(false);
                Player.instance.CanMove = false;
                Player.instance.CanAttack = false;
                Cursor.visible = true;
                EnemiesManager.SetActive(false);
                break;
            case GameState.Pause:

                isPaused = !isPaused;
                if (isPaused)
                {
                    playMusic();

                    GameUI.SetActive(false);
                    pauseUI.SetActive(true);
                    Player.instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Player.instance.CanMove = false;
                    Player.instance.CanAttack = false;
                    Cursor.visible = true;
                    EnemiesManager.SetActive(false);
                }
                else
                {
                    playMusic();
                    GameUI.SetActive(true);
                    pauseUI.SetActive(false);
                    Player.instance.CanMove = true;
                    Player.instance.CanAttack = true;
                    Cursor.visible = false;
                    EnemiesManager.SetActive(true);
                }
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changeState(5);
        }
    }
}
