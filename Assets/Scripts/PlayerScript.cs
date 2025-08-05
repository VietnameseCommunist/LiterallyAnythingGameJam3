using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;
    public PlayerDamage playerDamage;
    public PlayerCam playerCam;

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
        playerCam = GetComponentInChildren<PlayerCam>();
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
        Health -= damage;
        HealthUI.SetTo(Health);
    }
    void Die()
    {

    }

    public enum HoldingState { NotHolding,Holding}
}
