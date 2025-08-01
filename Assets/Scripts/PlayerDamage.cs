using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private Transform Camera;
  
    public GunData gunData;
    [SerializeField] float ShootingDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DealDamage();
        }
        Debug.DrawRay(Camera.position, Camera.forward);
    }
    void DealDamage()
    {
        Ray ray = new Ray(Camera.position, Camera.forward);
        //Debug.DrawRay(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, ShootingDistance, 1<<3))
        {
            hit.collider.GetComponent<Enemy>().GetDamage(gunData.Damage);
            Debug.Log(hit.collider.name);
        }
    }
}
