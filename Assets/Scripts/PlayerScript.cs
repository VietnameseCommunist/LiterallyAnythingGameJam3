using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;

    public GameObject HoldingObject;
    public bool IsHolding;
    public bool IsGun;

    [SerializeField] private int Health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null) { instance = this; }

        IsHolding = false;
        IsGun = false;

        Health = 100;
    }
    private void Awake()
    {
        if (instance == null) { instance = this; }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void GetDamage(int damage)
    {
        Health -= damage;
    }
}
