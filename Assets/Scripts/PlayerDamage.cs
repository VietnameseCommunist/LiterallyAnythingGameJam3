using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public static float ChargeRate; //for throwing
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
        else if (Input.GetKeyDown(KeyCode.F) && PlayerScript.instance.HoldState == PlayerScript.HoldingState.Holding)
        {
            DropObject();
            PlayerScript.instance.HoldingObject = null;
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                gun.Reload();
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
                PlayerCam.Instance.cam.fieldOfView -= Time.deltaTime * 6;
            }
            Percentage.value = ChargeRate;
            Percentage.max = 3;
        }
        if (Input.GetKeyUp(KeyCode.E) && PlayerScript.instance.HoldingObject != null)
        {
            PlayerScript.instance.HoldingObject.GetComponent<Collider>().enabled = true;
            float Ratio = ChargeRate / MaxRate;
            if (PlayerScript.instance.HoldingObject.GetComponent<Gun>() != null) PlayerScript.instance.HoldingObject.GetComponent<Gun>().DamageByThrowing(Ratio);
            else PlayerScript.instance.HoldingObject.GetComponent<Heal>().DamageByThrowing(Ratio);
            ChargeRate = 0;
            PlayerCam.Instance.cam.fieldOfView = PlayerCam.DefaultPOV;
        }
    }
    void PickUpObject()
    {
        if (Physics.Raycast(ray, out hit, 5, 1 << 9))
        {
            PlayerScript.instance.HoldingObject = hit.collider.gameObject;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            rb.useGravity = false;
            PlayerScript.instance.HoldState = PlayerScript.HoldingState.Holding;
            rb.isKinematic = true;

            PlayerScript.instance.HoldingObject.transform.position = Camera.position + Camera.rotation * new Vector3(0.5f, -0.3f, 0.5f);
            PlayerScript.instance.HoldingObject.transform.rotation = Camera.rotation;
            PlayerScript.instance.HoldingObject.GetComponent<Collider>().enabled = false;

            PlayerScript.instance.HoldingObject.gameObject.transform.parent.SetParent(PlayerCam.Instance.cam.transform);
        }
        else return;

        if (PlayerScript.instance.HoldingObject.GetComponent<Gun>() != null)
        {
            PlayerScript.instance.IsGun = true;
            Gun hehe = PlayerScript.instance.HoldingObject.GetComponent<Gun>();
            AmmunitionUI.ChangeAmmoCount(hehe.CurrentBullets, hehe.gunData.MaxBullets);

            switch (hehe.gunData.GunType)
            {
                case GunTypes.AssaultRifle: AmmunitionUI.ChangeAmmoDisplay(0); break;
                case GunTypes.ShotGun: AmmunitionUI.ChangeAmmoDisplay(1); break;
                case GunTypes.LightGun: AmmunitionUI.ChangeAmmoDisplay(2); break;
                case GunTypes.Sniper: AmmunitionUI.ChangeAmmoDisplay(3); break;
                default: AmmunitionUI.Instance.img.sprite = AmmunitionUI.Instance.defaulter; break;
            }
        }
        else
        {
            PlayerScript.instance.IsGun = false;
            AmmunitionUI.ChangeAmmoCount(0, 0);
        }
    }
    public void DropObject()
    {
        PlayerScript.instance.HoldingObject.gameObject.transform.parent.SetParent(null);
        PlayerScript.instance.HoldingObject.GetComponent<Collider>().enabled = true;
        PlayerScript.instance.HoldingObject.GetComponent<Collider>().excludeLayers = LayerMask.GetMask();
        PlayerScript.instance.IsGun = false;
        PlayerScript.instance.HoldState = PlayerScript.HoldingState.NotHolding;
        PlayerScript.instance.HoldingObject.GetComponent<Rigidbody>().useGravity = true;
        PlayerScript.instance.HoldingObject.GetComponent<Rigidbody>().isKinematic = false;

        AmmunitionUI.Instance.AmmoCount.text = "None";
        AmmunitionUI.Instance.img.sprite = AmmunitionUI.Instance.defaulter;
    }
}
