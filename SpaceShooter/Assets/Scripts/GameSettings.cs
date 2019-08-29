using UnityEngine;
using System.Collections;


[CreateAssetMenu(fileName ="Settings",menuName ="Game Settings")]
public class GameSettings : ScriptableObject
{
    public int startMaxShield=40;
    public int maximumShield = 100;
    public int startHealth=100;
    public int startMisiles=3;
    public int MaxMisiles = 10;
    public int roundDuration=60;
    public int EnemiesPerRound=20;
    public int lifes=3;
    public int lifesCost=5;
    public int shieldRechargeCost = 5;
    public int shieldCost = 5;
    public int projectilesCost = 5;
}
