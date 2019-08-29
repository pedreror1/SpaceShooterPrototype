﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public static Player Instance;
    public int Misiles = 3;
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
    private Text MisileText;
    [SerializeField]
    private Material ShieldMaterial;
    [SerializeField]
    private GameObject ShieldGO;
    private bool canBeDamaged = true;
    private WaitForSeconds damageCoolOff = new WaitForSeconds(3f);
    private Vector3 OriginalPlayerPosition;

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
        ShieldMaterial.SetColor("ShieldColor", Color.blue);
      GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = OriginalPlayerPosition;
        Health = GameManager.Instance.settings.startHealth;
        Misiles = GameManager.Instance.settings.startMisiles;
        currentShield = GameManager.Instance.MaxShield;
        UpdateUI();
        ShieldGO.SetActive(true);
    }
    public void UpdateUI()
    {
        
        healthBar.fillAmount = Health / 100f;
        shieldBar.fillAmount = currentShield / 100f;
        MisileText.text = Misiles.ToString();

    }
    
    void Start()
    {
        OriginalPlayerPosition = transform.position;
        Reset();
        
    }
    private void Awake()
    {
        Instance = this;
    }  
   
    // Update is called once per frame
    void Update()
    {
        if (timewithoutDamagae > shieldRecoveryRate && currentShield < GameManager.Instance.MaxShield)
        {
            currentShield += Time.deltaTime ;
            if(currentShield>0 && !ShieldGO.activeSelf)
            {
                ShieldGO.SetActive(true);
            }
            shieldBar.fillAmount = currentShield / 100f;
        }
        else
        {
            timewithoutDamagae += Time.deltaTime;
        }
        
    }
}
