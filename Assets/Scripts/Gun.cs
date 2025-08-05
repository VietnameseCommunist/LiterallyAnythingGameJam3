using Unity.VisualScripting;
using UnityEngine;

<<<<<<< HEAD
public class Gun : MonoBehaviour
=======
[System.Serializable]
public class Gun : MonoBehaviour, IReload
>>>>>>> e8b9d41bd1eb2c38374334652c8335703209056d
{

    public GunData gunData;
    private PlayerDamage playerDamage;
    public PlayerScript playerScript;

    public int CurrentBullets;

    public bool IsOnGround;
    bool Reloading;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDamage = PlayerScript.instance.playerDamage.GetComponent<PlayerDamage>();
        CurrentBullets = gunData.MaxBullets;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD

=======
        Heal();
>>>>>>> e8b9d41bd1eb2c38374334652c8335703209056d
    }
    public void Heal()
    {
        if (CurrentBullets <= 10 && Input.GetKeyDown(KeyCode.R) && playerScript.IsGun == true && Reloading != true)
        {
<<<<<<< HEAD
            ReloadingUI.StartReload(2);
            CurrentBullets = gunData.MaxBullets;
=======

             Debug.Log("Reload Detected");
            Reloading = true;
            Invoke("Reload", 2f);
>>>>>>> e8b9d41bd1eb2c38374334652c8335703209056d
            AmmunitionUI.ChangeAmmoCount(CurrentBullets, gunData.MaxBullets);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && CurrentBullets >= 0 && playerScript.IsGun == true && Reloading != true)
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
<<<<<<< HEAD
}
=======
    public void Reload()
    {
        CurrentBullets = gunData.MaxBullets;
        Reloading = false;
    }
}
public interface IReload
{
    void Reload();
}
>>>>>>> e8b9d41bd1eb2c38374334652c8335703209056d
