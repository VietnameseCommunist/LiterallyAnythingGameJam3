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

        if (Input.GetKeyDown(KeyCode.F) && PlayerScript.instance.HoldState == PlayerScript.HoldingState.NotHolding)
        {
            PickUpObject();
        }
        else if (PlayerScript.instance.HoldState == PlayerScript.HoldingState.Holding)
        {
            HoldObject();
        }

        if (PlayerScript.instance.HoldingObject == null) return;

        //main attack
        if (PlayerScript.instance.IsGun && PlayerScript.instance.HoldState == PlayerScript.HoldingState.Holding)
        {
            Gun gun = PlayerScript.instance.HoldingObject.GetComponent<Gun>();
            if (Input.GetMouseButtonDown(0))
            {
                gun.Heal();
            }
        }
        else if (!PlayerScript.instance.IsGun && PlayerScript.instance.HoldState == PlayerScript.HoldingState.Holding)
        {
            Heal heal = PlayerScript.instance.HoldingObject.GetComponent<Heal>();
            if (Input.GetMouseButtonDown(0))
            {
                heal.Damage();

            }
        }

        //throw
        if(Input.GetKey(KeyCode.E) && PlayerScript.instance.HoldState == PlayerScript.HoldingState.Holding)
        {
            if (ChargeRate <= 3)
            {
                ChargeRate += Time.deltaTime;
                PlayerScript.instance.playerCam.cam.fieldOfView -= Time.deltaTime * 6;
            }
        }
        if (Input.GetKeyUp(KeyCode.E) && PlayerScript.instance.HoldingObject != null)
        {
            PlayerScript.instance.HoldingObject.GetComponent<Collider>().enabled = true;
            float Ratio = ChargeRate / MaxRate;
            if (PlayerScript.instance.HoldingObject.GetComponent<Gun>() != null) PlayerScript.instance.HoldingObject.GetComponent<Gun>().DamageByThrowing(Ratio);
            else PlayerScript.instance.HoldingObject.GetComponent<Heal>().DamageByThrowing(Ratio);
            ChargeRate = 0;
            PlayerScript.instance.playerCam.cam.fieldOfView = PlayerCam.DefaultPOV;
        }
    }
    void PickUpObject()
    {
        if (Physics.Raycast(ray, out hit, 5, 1<<6))
        {
            PlayerScript.instance.HoldingObject = hit.collider.gameObject;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            rb.useGravity = false;
            PlayerScript.instance.HoldState = PlayerScript.HoldingState.Holding;
            rb.isKinematic = true;
        }
        else return;

        if (PlayerScript.instance.HoldingObject.GetComponent<Gun>() != null)
        {
            PlayerScript.instance.IsGun = true;
        }
        else PlayerScript.instance.IsGun = false;
    }
    void HoldObject()
    {
        PlayerScript.instance.HoldingObject.transform.position = Camera.position + Camera.rotation * new Vector3(0.5f, -0.2f, 1);
        PlayerScript.instance.HoldingObject.transform.rotation = Camera.rotation;
        PlayerScript.instance.HoldingObject.GetComponent<Collider>().enabled = false;

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerScript.instance.HoldingObject.GetComponent<Collider>().enabled = true;
            PlayerScript.instance.IsGun = false;
            PlayerScript.instance.HoldState = PlayerScript.HoldingState.NotHolding;
            PlayerScript.instance.HoldingObject.GetComponent<Rigidbody>().useGravity = true;
            PlayerScript.instance.HoldingObject.GetComponent <Rigidbody>().isKinematic = false;
            PlayerScript.instance.HoldingObject = null;
        }
    }
}
