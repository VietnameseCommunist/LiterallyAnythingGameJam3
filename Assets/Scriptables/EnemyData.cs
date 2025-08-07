using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int Health = 20;
    public int Damage = 10;
    public int Speed;
    public int AttackRate = 1;
}
