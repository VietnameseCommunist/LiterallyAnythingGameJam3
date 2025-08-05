using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public int Damage;
    public int ThrowDamage;
    public int Distance;

    public int MaxBullets;
    public enum GunTypes { AssaultRifle, ShotGun, LightGun, Sniper}
}
