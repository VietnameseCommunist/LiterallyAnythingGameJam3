using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public static PlayerScript instance;
    public PlayerDamage playerDamage;

    public GameObject HoldingObject;
    public bool IsGun;

    [SerializeField] private int Health;

    public HoldingState HoldState;

    [SerializeField] GameObject gameOverScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
         Time.timeScale = 1;
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
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) animator.SetBool("IsMoving", true);
        else animator.SetBool("IsMoving", false);
    }
    public void GetDamage(int damage)
    {
        if(Health <= 0)
        {
             if (gameOverScreen) Instantiate(gameOverScreen);
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
