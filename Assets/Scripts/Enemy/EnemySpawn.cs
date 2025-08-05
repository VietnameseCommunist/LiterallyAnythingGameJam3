using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] EnemiesToSpawn;
    private int MaximumEnemies;
    [SerializeField] private int CurrentEnemies;
    private float SpawnCoolDown;
    private float SpawnTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MaximumEnemies = 2;
        SpawnCoolDown = 2;
        SpawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (CurrentEnemies >= MaximumEnemies) return;

        if (SpawnTimer < SpawnCoolDown)
        {
            SpawnTimer += Time.deltaTime;
        }
        else Spawn();
    }
    void Spawn()
    {
        SpawnTimer = 0;
        int value;

        if (Random.value < 0.5f) // 50% chance
        {
            value = Random.Range(-20, -10); // upper bound is exclusive for ints
        }
        else
        {
            value = Random.Range(10, 21); // 21 to include 20
        }

        float Xpos = value + PlayerScript.instance.gameObject.transform.position.x;
        float Zpos = value + PlayerScript.instance.gameObject.transform.position.z;

        GameObject enemy = Instantiate(EnemiesToSpawn[0]);

        enemy.transform.position = new Vector3(Xpos,0,Zpos);
    }
}
