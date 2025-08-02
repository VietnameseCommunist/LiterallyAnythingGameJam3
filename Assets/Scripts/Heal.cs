using UnityEngine;

public class Heal : MonoBehaviour
{
    public HealData healData;
    private PlayerDamage playerDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage()
    {
        if (Physics.Raycast(playerDamage.ray, out playerDamage.hit, 2, 1 << 3))
        {
            playerDamage.hit.collider.GetComponent<Enemy>().GetDamage(healData.HealAmount);
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
}
