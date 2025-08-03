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
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);

        DistanceFromPlayer = Vector3.Distance(transform.position, PlayerScript.instance.transform.position);
        if(DistanceFromPlayer > 2)
        {
            agent.SetDestination(PlayerScript.instance.gameObject.transform.position);
        }
        else if(DistanceFromPlayer < 2)
        {
            agent.ResetPath();
        }
        Fight();
    }
    public void GetDamage(int damage)
    {
        Health -= damage;
    }
    void Fight()
    {
        Debug.DrawRay(transform.position, transform.forward);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5))
        { 
            if (hit.collider.tag == "Player")
            {
                Debug.Log(hit.collider.gameObject.name);
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
}
