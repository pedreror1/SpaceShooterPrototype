using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
   [SerializeField] Button LifesButton;
   [SerializeField] Button ShieldButton;
   [SerializeField] Button ShieldRechargeButton;
    [SerializeField] Button RocketButton;
    
    int lifesCost = 5;
    int shieldRechargeCost = 5;
    int shieldCost = 5;
    int projectilesCost = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        LifesButton.interactable =GameManager.Instance.Coins >= lifesCost && GameManager.Instance.lifes<3;
        RocketButton.interactable = GameManager.Instance.Coins >= projectilesCost && Player.Instance.projectiles<10;
        ShieldRechargeButton.interactable = GameManager.Instance.Coins >= shieldRechargeCost && Player.Instance.shieldRecoveryRate>1f ;
        ShieldButton.interactable = GameManager.Instance.Coins >= shieldCost && GameManager.Instance.MaxShield<100;

    }
    public void PurchaseLifes()
    {
        GameManager.Instance.Coins -= lifesCost;
        GameManager.Instance.lifes++;

    }
    public void PurchaseShield()
    {
        GameManager.Instance.Coins -= shieldCost;
        GameManager.Instance.MaxShield+=10;

    }
    public void PurchaseShieldRechargeRate()
    {
        GameManager.Instance.Coins -= shieldRechargeCost;
        Player.Instance.shieldRecoveryRate --;

    }
    public void PurchaseRockets()
    {
        GameManager.Instance.Coins-= projectilesCost;
        Player.Instance.projectiles++; ;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
