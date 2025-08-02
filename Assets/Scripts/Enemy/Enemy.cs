using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    [SerializeField] private int Health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = enemyData.Health;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GetDamage(int damage)
    {
        Health -= damage;
    }
}
