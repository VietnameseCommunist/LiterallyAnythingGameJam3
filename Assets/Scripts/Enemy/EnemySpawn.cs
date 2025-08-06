using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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
        MaximumEnemies = 1;
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
        Vector3 finalpos;
        int CurrentAttempt = 0;
        int MaxAttempt = 20;
        float Xpos;
        float Zpos;
        SpawnTimer = 0;
        do
        {
            if (Random.value < 0.5f) // 50% chance
                Xpos = Random.Range(-20, -10); // upper bound is exclusive for ints
            else
                Xpos = Random.Range(10, 21); // 21 to include 20

            if (Random.value < 0.5f)
                Zpos = Random.Range(-20, -10);
            else
                Zpos = Random.Range(10, 21);

            Xpos += PlayerScript.instance.gameObject.transform.position.x;
            Zpos += PlayerScript.instance.gameObject.transform.position.z;

            finalpos = new Vector3(Xpos, 0, Zpos);
            CurrentAttempt++;
        }
        while (!IsPositionOnNavMeshSurface(finalpos) && CurrentAttempt < MaxAttempt);

        if(CurrentAttempt >= MaxAttempt)
        {
            Debug.Log("Coudn't find a spawning position, after several tries.... Skipping to the next spawn");
            return;
        }
        GameObject Enemy = Instantiate(EnemiesToSpawn[0]);
        Enemy.transform.position = finalpos;
    }
    bool IsPositionOnNavMeshSurface(Vector3 position)
    {
        NavMeshHit hit;
        bool isNavMeshSurface = NavMesh.SamplePosition(position, out hit, 1, NavMesh.AllAreas);
        Debug.Log("Couldn't find a position to spawn, finding again...");
        return isNavMeshSurface;
    }
}
