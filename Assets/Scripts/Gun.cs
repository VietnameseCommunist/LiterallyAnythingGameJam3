using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    private PlayerDamage playerDamage;

    public bool IsOnGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Heal()
    {
        if (Physics.Raycast(playerDamage.ray, out playerDamage.hit, gunData.Distance, 1 << 3))
        {
            playerDamage.hit.collider.GetComponent<Enemy>().GetDamage(-gunData.Damage);
            Debug.Log(playerDamage.hit.collider.name);
        }
    }
    public void DamageByThrowing(float ChargeRateRatio)
    {
        if (playerDamage.playerScript.HoldingObject == null)
        {
            Debug.Log("Player is not holding anything");
            return;
        }

        Rigidbody rb = playerDamage.playerScript.HoldingObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        playerDamage.playerScript.IsHolding = false;
        playerDamage.playerScript.HoldingObject = null;
        Vector3 seeyuh = Quaternion.AngleAxis(-25, playerDamage.Camera.right) * playerDamage.Camera.forward;
        rb.AddForce(seeyuh * (500f * ChargeRateRatio));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsOnGround = true;
            return;
        }
        else IsOnGround = false;

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && !IsOnGround)
        {
            Enemy enemy = collision.collider.gameObject.GetComponent<Enemy>();
            enemy.GetDamage(-gunData.ThrowDamage);
        }
    }
}
