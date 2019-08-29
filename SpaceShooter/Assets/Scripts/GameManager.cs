using System;
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
    //IMPLEMENT THIS!
    [SerializeField] EnemyMG EnemiesManager;
    [SerializeField] GameObject pauseUI;
    public GameObject ExplosionParticle;
    public GameObject CoinPrefab;
    public CameraShake cameraShakeController;
    public int Score = 9999;
    public int Coins = 0;
    int enemiesKilled = 0;
    int TimeRemaining = 60;
    int startTimer = 3;
    public int lifes = 3;
    public float MaxShield = 40;
    public static GameManager Instance;
    [SerializeField] Text coinText,lifeText, timeText, startTimerText, enemiesKilledText,scoreText;
    WaitForSeconds timerCycleLapse = new WaitForSeconds(1f);
    int level = 1;
    public AudioSource inGameMusicSource;
    public AudioSource UIMusicSource;
    public AudioSource EffectsSource;
    Vector3 OriginalPlayerPosition;
    public AudioClip GameMusic;
    public AudioClip UIMusic;
    public AudioClip clickFX;
    public AudioClip countDownFX;
    public AudioClip startFX;
    public AudioClip welcomeFX;
    public AudioClip thanksFX;
    public AudioClip nextLevelFX;
    public AudioClip GameOverFX;
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
        resspawning = 6

    }
    bool isPaused = false;
    public GameState currentState = GameState.MainMenu;
    public void AddCoin(int numCoins = 1)
    {
        Coins += numCoins;
        UpdateUI();
    }
    public void UpdateUI()
    {
        coinText.text = Coins.ToString().PadLeft(2, '0');
        lifeText.text = lifes.ToString();
        scoreText.text = "Score: "+Score.ToString().PadLeft(5, '0');
        enemiesKilledText.text = enemiesKilled.ToString().PadLeft(3, '0');

    }
    public void AddScore(int points = 500)
    {
        Score += points;
        enemiesKilled++;
        UpdateUI();

    }
    // Start is called before the first frame update
    void Start()
    {
        OriginalPlayerPosition = Player.Instance.transform.position;
        playMusic();
        UpdateUI();
        Instance = this;
        cameraShakeController = GetComponent<CameraShake>();
    }

    IEnumerator GameTime(bool Respawning = false)
    {

        startTimerText.color = Color.white;
        startTimer = 3;
        startTimerText.text = "3";
       
       playSoundFX(countDownFX);
        startTimerText.GetComponent<Animator>().enabled = false;
         

        while (startTimer > 0)
        {
            startTimer--;

            yield return timerCycleLapse;

            if (startTimer > 0)
            {
                startTimerText.text = startTimer.ToString();
            }
            else
            {
                playSoundFX(startFX);
                startTimerText.text = "START!";
            }

        }
        EnemiesManager.Reset(Respawning);
        Player.Instance.CanMove = true;
        Player.Instance.CanAttack = true;

        startTimerText.GetComponent<Animator>().enabled = true;
        if (!Respawning)
        {
            TimeRemaining = 60;
            while (TimeRemaining > 0)
            {
                yield return timerCycleLapse;
                TimeRemaining--;
                timeText.text = TimeRemaining.ToString().PadLeft(2, '0');

            }
            if (lifes > 0)
            {
                Player.Instance.CanMove = false;
                Player.Instance.CanAttack = false;
                EnemiesManager.Pause();
                level++;
                changeState(2);
            }
        }
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
    void setupGameState(bool wasinShop=false)
    {
        playMusic(true);

        ShopUI.SetActive(false);

        StartCoroutine(GameTime());
        MainMenuUI.SetActive(false);
        GameUI.SetActive(true);

        Cursor.visible = false;
    }
    void setupHighscoreState()
    {
        Score = 0;
        Coins = 0;
        Player.Instance.UpdateUI();
        playMusic();
        ShopUI.SetActive(false);
        GameUI.SetActive(false);
        MainMenuUI.SetActive(false);

        HighScoreSaveUI.SetActive(false);
        HighscoreMainUI.SetActive(true);
        highscore.instance.LoadData(true);
    }
    void setupSaveHighscoreState()
    {
        playSoundFX(GameOverFX);
        Score += (Coins * 50);
        Coins = 0;
        Player.Instance.UpdateUI();
        EnemiesManager.GameOver();
        playMusic();
        ShopUI.SetActive(false);

        GameUI.SetActive(false);
        Player.Instance.CanMove = false;
        Player.Instance.CanAttack = false;
        Cursor.visible = true;
        HighScoreSaveUI.SetActive(true);
        EnemiesManager.gameObject.SetActive(false);
    }

    void setupMainMenu()
    {
        playMusic();
        pauseUI.SetActive(false);
        ShopUI.SetActive(false);

        HighScoreSaveUI.SetActive(false);
        HighscoreMainUI.SetActive(false);
        MainMenuUI.SetActive(true);
        GameUI.SetActive(false);
        Player.Instance.CanMove = false;
        Player.Instance.CanAttack = false;
        Cursor.visible = true;
        MaxShield = 40;
        Player.Instance.projectiles = 3;
        Player.Instance.Reset();
    }
    void setupShopState()
    {
        timeText.text = "60";
        playSoundFX(welcomeFX);
        playMusic();
        ShopUI.SetActive(true);

        GameUI.SetActive(false);
        Player.Instance.CanMove = false;
        Player.Instance.CanAttack = false;
        Cursor.visible = true;
        EnemiesManager.Pause();
    }
    private void setupPauseState()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            playMusic();

            GameUI.SetActive(false);
            pauseUI.SetActive(true);
            Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Player.Instance.CanMove = false;
            Player.Instance.CanAttack = false;
            Cursor.visible = true;
            EnemiesManager.gameObject.SetActive(false);
        }
        else
        {
            playMusic();
            GameUI.SetActive(true);
            pauseUI.SetActive(false);
            Player.Instance.CanMove = true;
            Player.Instance.CanAttack = true;
            Cursor.visible = false;
            EnemiesManager.gameObject.SetActive(true);
        }
    }

    private void setupRespawnState()
    {
        lifes--;
        UpdateUI();


        EnemiesManager.Pause();
        if (lifes <= 0)
        {
            changeState((int)GameState.HighScoreSave);
        }
        else
        {
            //IMPLEMENT THIS!
            Player.Instance.Reset();

            StartCoroutine(GameTime(true));
            Player.Instance.CanMove = false;
            Player.Instance.CanAttack = false;
        }
    }
    public void changeState(int newState)
    {
        currentState = (GameState)newState;
        switch (currentState)
        {
            case GameState.Game:
                setupGameState();
                break;
            case GameState.HighScoreSave:
                setupSaveHighscoreState();
                break;
            case GameState.HighScorescreen:
                setupHighscoreState();
                break;
            case GameState.MainMenu:
                setupMainMenu();

                break;
            case GameState.Shop:
                setupShopState();
                break;
            case GameState.Pause:

                setupPauseState();
                break;
            case GameState.resspawning:
                setupRespawnState();
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