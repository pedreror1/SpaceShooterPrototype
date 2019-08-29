using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
   [SerializeField]
    private Button LifesButton;
   [SerializeField]
    private Button ShieldButton;
   [SerializeField]
    private Button ShieldRechargeButton;
   [SerializeField]
    private Button RocketButton;
    
    private int lifesCost = 5;
    private int shieldRechargeCost = 5;
    private int shieldCost = 5;
    private int projectilesCost = 5;
    // Start is called before the first frame update
   
    private void OnEnable()
    {

        LifesButton.interactable =GameManager.Instance.Coins >= lifesCost && GameManager.Instance.lifes<3;
        RocketButton.interactable = GameManager.Instance.Coins >= projectilesCost && Player.Instance.projectiles<10;
        ShieldRechargeButton.interactable = GameManager.Instance.Coins >= shieldRechargeCost && Player.Instance.shieldRecoveryRate>1f ;
        ShieldButton.interactable = GameManager.Instance.Coins >= shieldCost && GameManager.Instance.MaxShield<100;

    }
    public void PurchaseLifes()
    {
        GameManager.Instance.playSoundFX(GameManager.Instance.thanksFX);
        GameManager.Instance.Coins -= lifesCost;
        GameManager.Instance.lifes++;

    }
    public void PurchaseShield()
    {
        GameManager.Instance.playSoundFX(GameManager.Instance.thanksFX);
        GameManager.Instance.Coins -= shieldCost;
        GameManager.Instance.MaxShield+=10;

    }
    public void PurchaseShieldRechargeRate()
    {
        GameManager.Instance.playSoundFX(GameManager.Instance.thanksFX);
        GameManager.Instance.Coins -= shieldRechargeCost;
        Player.Instance.shieldRecoveryRate --;

    }
    public void PurchaseRockets()
    {
        GameManager.Instance.playSoundFX(GameManager.Instance.thanksFX);
        GameManager.Instance.Coins-= projectilesCost;
        Player.Instance.projectiles++; ;

    }
     
}
