using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public int Damage;
    public int ThrowDamage;
    public int Distance;
    public float AttackRate;
    public int Recoil;
    public int MaxBullets;
    public GunTypes GunType;
}
public enum GunTypes { AssaultRifle, ShotGun, LightGun, Sniper }
