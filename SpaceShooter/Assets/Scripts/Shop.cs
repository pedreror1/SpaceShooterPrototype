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
    
   
    // Start is called before the first frame update
    private void Awake()
    {
 
      
       
    }
    private void OnEnable()
    {

        LifesButton.interactable =GameManager.Instance.Coins >= GameManager.Instance.settings.lifesCost &&
                                  GameManager.Instance.lifes < GameManager.Instance.settings.lifes;

        RocketButton.interactable = GameManager.Instance.Coins >= GameManager.Instance.settings.projectilesCost && 
                                    Player.Instance.Misiles < GameManager.Instance.settings.MaxMisiles;

        ShieldRechargeButton.interactable = GameManager.Instance.Coins >= GameManager.Instance.settings.shieldRechargeCost && 
                                            Player.Instance.shieldRecoveryRate>1f ;

        ShieldButton.interactable = GameManager.Instance.Coins >= GameManager.Instance.settings.shieldCost &&
                                    GameManager.Instance.MaxShield < GameManager.Instance.settings.maximumShield;

    }
    public void PurchaseLifes()
    {
        GameManager.Instance.playSoundFX(GameManager.Instance.thanksFX);
        GameManager.Instance.Coins -= GameManager.Instance.settings.lifesCost;
        GameManager.Instance.lifes++;

    }
    public void PurchaseShield()
    {
        GameManager.Instance.playSoundFX(GameManager.Instance.thanksFX);
        GameManager.Instance.Coins -= GameManager.Instance.settings.shieldCost;
        GameManager.Instance.MaxShield+=10;

    }
    public void PurchaseShieldRechargeRate()
    {
        GameManager.Instance.playSoundFX(GameManager.Instance.thanksFX);
        GameManager.Instance.Coins -= GameManager.Instance.settings.shieldRechargeCost;
        Player.Instance.shieldRecoveryRate --;

    }
    public void PurchaseRockets()
    {
        GameManager.Instance.playSoundFX(GameManager.Instance.thanksFX);
        GameManager.Instance.Coins-= GameManager.Instance.settings.projectilesCost;
        Player.Instance.Misiles++; ;

    }
     
}
