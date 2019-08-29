using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public static Player Instance;
    public int projectiles = 3;
    public float shieldRecoveryRate = 10f;
    public bool CanMove = false;
    public bool CanAttack = false;

    private int Health;
    private float currentShield;
    [SerializeField]
    private Image shieldBar;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Material ShieldMaterial;
    [SerializeField]
    private GameObject ShieldGO;
    private bool canBeDamaged = false;
    private WaitForSeconds damageCoolOff = new WaitForSeconds(3f);
    private float timewithoutDamagae = 0f;
    IEnumerator DamageCoolDown()
    {
        yield return damageCoolOff;
        canBeDamaged = true;
    }
    public void getDamage(int damage)
    {
        if (canBeDamaged)
        {
            StartCoroutine(DamageCoolDown());
            GameManager.Instance.cameraShakeController.ShakeCamera(4.5f, 0.33f);

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
                ShieldMaterial.SetColor("ShieldColor", Color.Lerp(Color.blue, Color.red, currentShield / GameManager.Instance.MaxShield));
            }
            UpdateUI();
        }
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
    
    void Start()
    {
        
        Health = 100;
        currentShield = GameManager.Instance.MaxShield;
        
    }
    private void Awake()
    {
        Instance = this;
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
