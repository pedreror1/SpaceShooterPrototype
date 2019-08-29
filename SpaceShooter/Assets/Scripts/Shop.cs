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
    [SerializeField]
    private Text LifesText;
    [SerializeField]
    private Text shieldText;
    [SerializeField]
    private Text shieldRechargeText;
    [SerializeField]
    private Text rocketText;

    // Start is called before the first frame update
    private void Awake()
    {
 
      
       
    }
    private void OnEnable()
    {
        LifesText.text = GameManager.Instance.settings.lifesCost.ToString();
        shieldText.text = GameManager.Instance.settings.shieldCost.ToString();
        shieldRechargeText.text = GameManager.Instance.settings.shieldRechargeCost.ToString();
        rocketText.text = GameManager.Instance.settings.projectilesCost.ToString();

        validateButtons();

    }
    void validateButtons()
    {
        LifesButton.interactable = GameManager.Instance.Coins >= GameManager.Instance.settings.lifesCost &&
                                GameManager.Instance.lifes < GameManager.Instance.settings.lifes;

        RocketButton.interactable = GameManager.Instance.Coins >= GameManager.Instance.settings.projectilesCost &&
                                    Player.Instance.Misiles < GameManager.Instance.settings.MaxMisiles;

        ShieldRechargeButton.interactable = GameManager.Instance.Coins >= GameManager.Instance.settings.shieldRechargeCost &&
                                            Player.Instance.shieldRecoveryRate > 1f;

        ShieldButton.interactable = GameManager.Instance.Coins >= GameManager.Instance.settings.shieldCost &&
                                    GameManager.Instance.MaxShield < GameManager.Instance.settings.maximumShield;
    }
    public void PurchaseLifes()
    {
        GameManager.Instance.playSoundFX(GameManager.Instance.thanksFX);
        GameManager.Instance.Coins -= GameManager.Instance.settings.lifesCost;
        GameManager.Instance.lifes++;
        LifesButton.interactable = GameManager.Instance.Coins >= GameManager.Instance.settings.lifesCost &&
                                  GameManager.Instance.lifes < GameManager.Instance.settings.lifes;
        GameManager.Instance.UpdateUI();
        validateButtons();
    }
    public void PurchaseShield()
    {
        GameManager.Instance.playSoundFX(GameManager.Instance.thanksFX);
        GameManager.Instance.Coins -= GameManager.Instance.settings.shieldCost;
        GameManager.Instance.MaxShield+=10;
        ShieldButton.interactable = GameManager.Instance.Coins >= GameManager.Instance.settings.shieldCost &&
                                  GameManager.Instance.MaxShield < GameManager.Instance.settings.maximumShield;
        GameManager.Instance.UpdateUI();
        validateButtons();
    }
    public void PurchaseShieldRechargeRate()
    {
        GameManager.Instance.playSoundFX(GameManager.Instance.thanksFX);
        GameManager.Instance.Coins -= GameManager.Instance.settings.shieldRechargeCost;
        Player.Instance.shieldRecoveryRate --;
        ShieldRechargeButton.interactable = GameManager.Instance.Coins >= GameManager.Instance.settings.shieldRechargeCost &&
                                           Player.Instance.shieldRecoveryRate > 1f;
        GameManager.Instance.UpdateUI();
        validateButtons();
    }
    public void PurchaseRockets()
    {
        GameManager.Instance.playSoundFX(GameManager.Instance.thanksFX);
        GameManager.Instance.Coins-= GameManager.Instance.settings.projectilesCost;
        Player.Instance.Misiles++; ;
        RocketButton.interactable = GameManager.Instance.Coins >= GameManager.Instance.settings.projectilesCost &&
                                   Player.Instance.Misiles < GameManager.Instance.settings.MaxMisiles;
        GameManager.Instance.UpdateUI();
        validateButtons();
    }
     
}
