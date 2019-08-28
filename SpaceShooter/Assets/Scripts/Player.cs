using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public static Player Instance;
    public bool canShootProjectiles = true;

    int Health;
    float currentShield;
    public int projectiles = 3;
    public float shieldRecoveryRate = 10f;
    [SerializeField] Image shieldBar, healthBar;
    enum GameState
    {
        MainMenu,
        HighScores,
        Game,
        Shop
    }
    public bool CanMove=false;
    public bool CanAttack=false;
    float timewithoutDamagae = 0f;
    [SerializeField] GameObject ShieldGO;
   
    public void getDamage(int damage)
    {
        timewithoutDamagae = 0f;
        if (currentShield <= 0)
        {
            ShieldGO.SetActive(false);
            Health -= damage;
            if (Health <= 0)
            {
                Instantiate(GameManager.Instance.ExplosionParticle, transform.position, Quaternion.identity);
                GameManager.Instance.changeState(6);
            }
        }
        else
        {
           currentShield -= damage;
        }
        UpdateUI();
    }
    public void Reset()
    {
        Health = 100;
        currentShield = GameManager.Instance.MaxShield;
        ShieldGO.SetActive(true);
    }
    public void UpdateUI()
    {
        healthBar.fillAmount = Health / 100f;
        shieldBar.fillAmount = currentShield / 100f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        Health = 100;
        currentShield = GameManager.Instance.MaxShield;
        
    }
    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        //print(other.transform.name);
    }
    void dead()
    {
        GameManager.Instance.changeState(4);
    }
    // Update is called once per frame
    void Update()
    {
        if (timewithoutDamagae > shieldRecoveryRate && currentShield<GameManager.Instance.MaxShield)
        {
            currentShield += Time.deltaTime * 10f;
            
            shieldBar.fillAmount = currentShield / 100f;
        }
        
    }
}
