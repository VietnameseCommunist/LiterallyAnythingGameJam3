using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class Gun : MonoBehaviour
{
    [SerializeField] private AudioSource audiosource;
    public GunData gunData;
    private PlayerDamage playerDamage;

    public bool IsReload = false;
    float AttackRateFill;
    public int CurrentBullets;

    public bool IsOnGround;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDamage = PlayerScript.instance.playerDamage.GetComponent<PlayerDamage>();
        CurrentBullets = gunData.MaxBullets;
        AttackRateFill = gunData.AttackRate;
    }
    // Update is called once per frame
    void Update()
    {
        if(AttackRateFill < gunData.AttackRate)
        {
            AttackRateFill += Time.deltaTime;
        }
    }
    public void Heal()
    {
        if (AttackRateFill < gunData.AttackRate) return;

        if (CurrentBullets <= 0)
        {
            Reload();
            return;
        }
        else if (!IsReload)
        {
            CurrentBullets--;
            audiosource.time = 0.1f;
            audiosource.Play();
            Recoil(gunData.Recoil);
            AmmunitionUI.ChangeAmmoCount(CurrentBullets, gunData.MaxBullets);
        }

        if (IsReload) return;

        AttackRateFill = 0;

        if (Physics.Raycast(playerDamage.ray, out playerDamage.hit, gunData.Distance, 1 << 3))
        {
            playerDamage.hit.collider.GetComponentInParent<Enemy>().GetDamage(gunData.Damage);
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
            gameObject.GetComponent<Collider>().excludeLayers = LayerMask.GetMask("Player", "Enemy");
            return;
        }
        IsOnGround = false;

        if (IsOnGround) return;

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && !IsOnGround)
        {
            Enemy enemy = collision.collider.gameObject.GetComponentInParent<Enemy>();
            enemy.GetDamage(-gunData.ThrowDamage);
            MarkerMaker.Marker(3).transform.localScale *= (gunData.ThrowDamage / 3);
        }
    }
    IEnumerator ReloadCoroutine()
    {
        IsReload = true;
        yield return new WaitForSeconds(2);
        if (ReloadingUI.interruptus) yield break;
        if (ReloadingUI.Instance.img.fillAmount > 0.9f)
        {
            CurrentBullets = gunData.MaxBullets;
            AmmunitionUI.ChangeAmmoCount(CurrentBullets, gunData.MaxBullets);
            IsReload = false;
        }
    }
    public void Reload()
    {
        ReloadingUI.StartReload(2);
        StartCoroutine(ReloadCoroutine());
    }
    void Recoil(int AmountOfRecoil)
    {
        PlayerCam.Instance.XrotationChange(AmountOfRecoil);
    }
}
