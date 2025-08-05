using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int Health = 100;
    public int Damage = 5;
    public int Speed;
    public int AttackRate = 1;
}
