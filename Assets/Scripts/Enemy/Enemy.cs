using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public EnemyData enemyData;
    [SerializeField] private int Health;

    private float AttackRateFill;
    [SerializeField] private float DistanceFromPlayer;

    Ray ray;
    RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = enemyData.Health;
        agent = GetComponentInChildren<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);

        Moving();
        Fight();
    }
    public void GetDamage(int damage)
    {
        if (Health > 0) Health -= damage;
        else Die();
    }
    void Die()
    {
        Destroy(gameObject);
    }
    void Fight()
    {
        Debug.DrawRay(agent.gameObject.transform.position, agent.gameObject.transform.forward);
        Ray ray = new Ray(agent.gameObject.transform.position, agent.gameObject.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5))
        { 
            if (hit.collider.tag == "Player")
            {
                if (AttackRateFill >= enemyData.AttackRate)
                {
                    if (hit.collider == null) Debug.Log("There is no player");
                    PlayerScript.instance.GetDamage(enemyData.Damage);
                    Debug.Log("Got hit uwu");
                    AttackRateFill = 0;
                }
                else AttackRateFill += Time.deltaTime;
            }
        }
    }
    void FaceThePlayer()
    {
        Vector3 DirectionToPlayer = (PlayerScript.instance.transform.position - agent.gameObject.transform.position).normalized;
        DirectionToPlayer.y = 0;
        agent.gameObject.transform.rotation = Quaternion.LookRotation(DirectionToPlayer);
    }
    void Moving()
    {
        DistanceFromPlayer = Vector3.Distance(PlayerScript.instance.gameObject.transform.position, agent.gameObject.transform.position);
        if (DistanceFromPlayer > 2)
        {
            agent.SetDestination(PlayerScript.instance.gameObject.transform.position);
        }
        else if (DistanceFromPlayer < 2)
        {
            agent.ResetPath();
            FaceThePlayer();
        }
    }
}
