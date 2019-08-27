using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public static Player instance;
    public bool canShootProjectiles = true;

    int Health;
    float currentShield;
    float  MaxShield = 40;
    public float shieldRecoveryRate = 1f;
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
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Health = 100;
        currentShield = MaxShield;
        healthBar.fillAmount = Health / 100f;
        shieldBar.fillAmount = currentShield / 100f;
    }
    void getDamaged()
    {
        timewithoutDamagae = 0f;
        healthBar.fillAmount = Health / 100f;
        shieldBar.fillAmount = currentShield / 100f;
    }
    private void OnTriggerEnter(Collider other)
    {
        //print(other.transform.name);
    }
    void dead()
    {
        GameManager.instance.changeState(4);
    }
    // Update is called once per frame
    void Update()
    {
        if (timewithoutDamagae > 10f && currentShield<MaxShield)
        {
            currentShield += Time.deltaTime * shieldRecoveryRate;
            
            shieldBar.fillAmount = currentShield / 100f;
        }
        if(Health<=0)
        {
            dead();
        }
    }
}
