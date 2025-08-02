using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public PlayerScript playerScript;

    [SerializeField] private float ChargeRate; //for throwing
    [SerializeField] private float MaxRate = 3;

    public Transform Camera;
    public Transform Hand;

    public Ray ray;
    public RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(Camera.position, Camera.forward);
        Debug.DrawRay(Camera.position, Camera.forward);

        if (Input.GetKeyDown(KeyCode.E) && !playerScript.IsHolding)
        {
            PickUpObject();
        }
        else if (playerScript.IsHolding)
        {
            HoldObject();
        }

        if (playerScript.HoldingObject == null) return;

        //main attack
        if (playerScript.IsGun && playerScript.IsHolding)
        {
            Gun gun = playerScript.HoldingObject.GetComponent<Gun>();
            if (Input.GetMouseButtonDown(0))
            {
                gun.Heal();
            }
        }
        else if (!playerScript.IsGun && playerScript.IsHolding)
        {
            Heal heal = playerScript.HoldingObject.GetComponent<Heal>();
            if (Input.GetMouseButtonDown(0))
            {
                heal.Damage();
            }
        }

        //throw
        if(Input.GetKey(KeyCode.Q) && playerScript.IsHolding)
        {
            if(ChargeRate <= 3) ChargeRate += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            float Ratio = ChargeRate / MaxRate;
            if (playerScript.HoldingObject.GetComponent<Gun>() != null) playerScript.HoldingObject.GetComponent<Gun>().DamageByThrowing(Ratio);
            else playerScript.HoldingObject.GetComponent<Heal>().DamageByThrowing(Ratio);
            ChargeRate = 0;
        }
    }
    void PickUpObject()
    {
        if (Physics.Raycast(ray, out hit, 5))
        {
            playerScript.HoldingObject = hit.collider.gameObject;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            rb.useGravity = false;
            playerScript.IsHolding = true;
            rb.isKinematic = true;
        }

        if(playerScript.HoldingObject.GetComponent<Gun>() != null)
        {
            playerScript.IsGun = true;
        }
        else playerScript.IsGun = false;
    }
    void HoldObject()
    {
        playerScript.HoldingObject.transform.position = Camera.position + Camera.rotation * new Vector3(0.5f, -0.2f, 1);
        playerScript.HoldingObject.transform.rotation = Camera.rotation;

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerScript.IsGun = false;
            playerScript.IsHolding = false;
            playerScript.HoldingObject.GetComponent<Rigidbody>().useGravity = true;
            playerScript.HoldingObject.GetComponent <Rigidbody>().isKinematic = false;
            playerScript.HoldingObject = null;
        }
    }
}
