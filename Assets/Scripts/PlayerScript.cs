using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;
    public PlayerDamage playerDamage;

    public GameObject HoldingObject;
    public bool IsGun;

    [SerializeField] private int Health;

    public HoldingState HoldState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null) { instance = this; }
        else
        {
            Destroy(instance);
            if (instance == null) { instance = this; }
        }

        HoldState = HoldingState.NotHolding;
        IsGun = false;

        Health = 100;
        playerDamage = GetComponent<PlayerDamage>();
    }

    void Start()
    {
         GradientOverlay.MakeGradient(0, Color.black);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void GetDamage(int damage)
    {
        if(Health <= 0)
        {
            Die();
        }
        Health -= damage;
        HealthUI.SetTo(Health);
        Gradient.Instance.ColorToRedWhenDamage();
    }
    void Die()
    {
        PlayerCam.Instance.cam.gameObject.transform.SetParent(null);
        Destroy(PlayerScript.instance.gameObject);
    }

    public enum HoldingState { NotHolding,Holding}
}
