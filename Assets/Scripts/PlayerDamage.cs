using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float ChargeRate; //for throwing
    [SerializeField] private float MaxRate = 3;

    public Transform Camera;

    public Ray ray;
    public RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(Camera.position, Camera.forward);
        Debug.DrawRay(Camera.position, Camera.forward);

        if (Input.GetKeyDown(KeyCode.F) && !PlayerScript.instance.IsHolding)
        {
            PickUpObject();
        }
        else if (PlayerScript.instance.IsHolding)
        {
            HoldObject();
        }

        if (PlayerScript.instance.HoldingObject == null) return;

        //main attack
        if (PlayerScript.instance.IsGun && PlayerScript.instance.IsHolding)
        {
            Gun gun = PlayerScript.instance.HoldingObject.GetComponent<Gun>();
            if (Input.GetMouseButtonDown(0))
            {
                gun.Heal();
            }
        }
        else if (!PlayerScript.instance.IsGun && PlayerScript.instance.IsHolding)
        {
            Heal heal = PlayerScript.instance.HoldingObject.GetComponent<Heal>();
            if (Input.GetMouseButtonDown(0))
            {
                heal.Damage();
            }
        }

        //throw
        if(Input.GetKey(KeyCode.E) && PlayerScript.instance.IsHolding)
        {
            if(ChargeRate <= 3) ChargeRate += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            float Ratio = ChargeRate / MaxRate;
            if (PlayerScript.instance.HoldingObject.GetComponent<Gun>() != null) PlayerScript.instance.HoldingObject.GetComponent<Gun>().DamageByThrowing(Ratio);
            else PlayerScript.instance.HoldingObject.GetComponent<Heal>().DamageByThrowing(Ratio);
            ChargeRate = 0;
        }
    }
    void PickUpObject()
    {
        if (Physics.Raycast(ray, out hit, 5))
        {
            PlayerScript.instance.HoldingObject = hit.collider.gameObject;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            rb.useGravity = false;
            PlayerScript.instance.IsHolding = true;
            rb.isKinematic = true;
        }

        if(PlayerScript.instance.HoldingObject.GetComponent<Gun>() != null)
        {
            PlayerScript.instance.IsGun = true;
        }
        else PlayerScript.instance.IsGun = false;
    }
    void HoldObject()
    {
        PlayerScript.instance.HoldingObject.transform.position = Camera.position + Camera.rotation * new Vector3(0.5f, -0.2f, 1);
        PlayerScript.instance.HoldingObject.transform.rotation = Camera.rotation;

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerScript.instance.IsGun = false;
            PlayerScript.instance.IsHolding = false;
            PlayerScript.instance.HoldingObject.GetComponent<Rigidbody>().useGravity = true;
            PlayerScript.instance.HoldingObject.GetComponent <Rigidbody>().isKinematic = false;
            PlayerScript.instance.HoldingObject = null;
        }
    }
}
