using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public static PlayerScript instance;
    public PlayerDamage playerDamage;

    public GameObject HoldingObject;
    private float TimeSinceLastAttackedByEnemy;
    private float TimeToHeal;
    public bool IsGun;

    public int _Health;
    private int Health
    {
        get {  return _Health; }
        set
        {
            if(_Health != value)
            {
                _Health = value;
                OnHealthChanged();
            }
        }
    }
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

        _Health = 100;
        playerDamage = GetComponent<PlayerDamage>();

        TimeToHeal = 5;

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

        if (TimeSinceLastAttackedByEnemy < TimeToHeal)
        {
            TimeSinceLastAttackedByEnemy += Time.deltaTime;
        }
    }
    public void GetDamage(int damage)
    {
        if(Health <= 0)
        {
             if (gameOverScreen) Instantiate(gameOverScreen);
            Die();
        }
        Health -= damage;
        TimeSinceLastAttackedByEnemy = 0;
        StartCoroutine(NaturalHealing(5));
        Gradient.Instance.ColorToRedWhenDamage();
    }
    void Die()
    {
        PlayerCam.Instance.cam.gameObject.transform.SetParent(null);
        Destroy(PlayerScript.instance.gameObject);
    }
    bool OnHealthChanged()
    {
        HealthUI.SetTo(Health);
        return true;
    }
    bool IsHealing()
    {
        if(TimeSinceLastAttackedByEnemy > 5)
        {
            return true;
        }
        return false;
    }
    IEnumerator NaturalHealing(int HealPerSecond)
    {
        yield return new WaitForSeconds(TimeToHeal);
        do
        {
            if(!IsHealing()) yield break;
            Health += HealPerSecond;
            Debug.Log("seeyuh");
            if (Health >= 100)
            {
                Health = 100;
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
        while (Health < 100 && IsHealing());
    }
    public enum HoldingState { NotHolding,Holding}
}
