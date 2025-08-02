using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private GameObject HoldingObject;
    [SerializeField] private bool IsHolding;

    [SerializeField] private Transform Camera;
    [SerializeField] private Transform Hand;
  
    public GunData gunData;
    [SerializeField] float ShootingDistance;

    Ray ray;
    RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsHolding = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(Camera.position, Camera.forward);
        if (Input.GetMouseButtonDown(0))
        {
            DealDamage();
        }
        Debug.DrawRay(Camera.position, Camera.forward);

        if (Input.GetKeyDown(KeyCode.E) && !IsHolding)
        {
            PickUpObject();
        }
        else if(IsHolding)
        {
            HoldObject();
        }

    }
    void DealDamage()
    {
        if(Physics.Raycast(ray, out hit, ShootingDistance, 1<<3))
        {
            hit.collider.GetComponent<Enemy>().GetDamage(gunData.Damage);
            Debug.Log(hit.collider.name);
        }
    }
    void PickUpObject()
    {
        if (Physics.Raycast(ray, out hit, 5, 1 << 7))
        {
            HoldingObject = hit.collider.gameObject;
            hit.collider.GetComponent<Rigidbody>().useGravity = false;
            IsHolding = true;
            
        }
    }
    void HoldObject()
    {
        HoldingObject.transform.position = Camera.position + Camera.rotation * new Vector3(0.5f, -0.2f, 1);
        HoldingObject.transform.rotation = Camera.rotation;

        if (Input.GetKeyDown(KeyCode.E))
        {
            IsHolding = false;
            HoldingObject.GetComponent<Rigidbody>().useGravity = true;
            HoldingObject = null;
        }
    }
}
