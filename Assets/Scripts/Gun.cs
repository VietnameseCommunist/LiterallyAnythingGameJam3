using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour, IReload
{
    public GunData gunData;
    private PlayerDamage playerDamage;

    public int CurrentBullets;

    public bool IsOnGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDamage = PlayerScript.instance.playerDamage.GetComponent<PlayerDamage>();
        CurrentBullets = gunData.MaxBullets;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Heal()
    {
        if (CurrentBullets <= 0)
        {
            Reload();
            AmmunitionUI.ChangeAmmoCount(CurrentBullets, gunData.MaxBullets);
            return;
        }
        else
        {
            CurrentBullets--;
            AmmunitionUI.ChangeAmmoCount(CurrentBullets, gunData.MaxBullets);
        }

        if (Physics.Raycast(playerDamage.ray, out playerDamage.hit, gunData.Distance, 1 << 3))
        {
            playerDamage.hit.collider.GetComponentInParent<Enemy>().GetDamage(-gunData.Damage);
            Debug.Log(playerDamage.hit.collider.name);
        }
    }
    public void DamageByThrowing(float ChargeRateRatio)
    {
        if (PlayerScript.instance.HoldingObject == null)
        {
            Debug.Log("Player is not holding anything");
            return;
        }

        playerDamage.DropObject();
        Vector3 seeyuh = Quaternion.AngleAxis(-25, playerDamage.Camera.right) * playerDamage.Camera.forward;
        PlayerScript.instance.HoldingObject.GetComponent<Rigidbody>().AddForce(seeyuh * (500f * ChargeRateRatio));
        PlayerScript.instance.HoldingObject = null;
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
            Enemy enemy = collision.collider.gameObject.GetComponentInParent<Enemy>();
            enemy.GetDamage(-gunData.ThrowDamage);
        }
    }
    public void Reload()
    {
        CurrentBullets = gunData.MaxBullets;
    }
}
public interface IReload
{
    void Reload();
}
