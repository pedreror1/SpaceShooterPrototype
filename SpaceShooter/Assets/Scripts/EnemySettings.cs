using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="Enemy Settings",menuName ="Enemies")]
public class EnemySettings : ScriptableObject
{
    public int speed = 100;
    public float bulletCoolDown = 2.5f;
    public int Health = 100;
    public int FOVRadius = 250;
    public Material shipColor;
    public int value = 500;
    public int distToShoot = 30;

}
