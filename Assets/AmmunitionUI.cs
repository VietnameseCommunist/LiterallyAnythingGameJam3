using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class AmmunitionUI : MonoBehaviour
{
     public static AmmunitionUI Instance;
     public static bool redo;

     public Vector3 startPosition;
     public Vector3 firstChildPos;

     public Sprite[] ammoSprites;
     public Image img;

     public Text AmmoCount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         if (!Instance) Instance = this;
         startPosition = transform.position;
         firstChildPos = transform.GetChild(0).transform.position;
         ChangeAmmoDisplay(0);
         ChangeAmmoCount(200);
    }

    void Update()
    {
         if (Input.GetKeyDown(KeyCode.P))
         {
              ShootEffect(600f);
         }
    }

    // Update is called once per frame
    public static void ShootEffect(float recoil)
    {
         if (Instance && recoil > 0) Instance.StartCoroutine(Instance.Recoil(recoil));
    }

    public static void ChangeAmmoCount(int number)
    {
         if (Instance)
         {
              if (Instance.AmmoCount)
              {
                  Instance.AmmoCount.text = number.ToString();
              }
         }
    }

    //Assault
    //Shotgun
    //Pistol
    //Sniper
    public static void ChangeAmmoDisplay(byte num)
    {
         if (Instance)
         if (num < Instance.ammoSprites.Length)
         Instance.img.sprite = Instance.ammoSprites[num];
    }

    public IEnumerator Recoil(float recoil)
    {
         transform.position = startPosition;
         if (transform.childCount > 0)
         {
              transform.GetChild(0).transform.position = firstChildPos;
         }
         float goThrough = recoil;
         float offset = 0;
         float modifier = Mathf.Clamp(recoil, 100f, 500f);
         redo = false;
         yield return null;
         redo = true;
         while (goThrough > 0)
         {
              if (!redo)
              {
                   redo = true;
                   yield break;
              }
              goThrough -= modifier * Time.deltaTime;
              if (offset < 100)
              {
                   offset += modifier * Time.deltaTime;
                   transform.position += new Vector3(modifier * Time.deltaTime, 0, 0);
                   if (transform.childCount > 0)
                   {
                        transform.GetChild(0).transform.position += new Vector3(modifier * Time.deltaTime, 0, 0) / 1.2f;
                   }
              }
              yield return null;
         }
         while (offset > 0)
         {
              if (!redo)
              {
                   redo = true;
                   yield break;
              }
              transform.position -= new Vector3(75 * 0.05f, 0, 0);
              if (transform.childCount > 0)
              {
                   transform.GetChild(0).transform.position -= new Vector3(modifier * Time.deltaTime, 0, 0) / 1.2f;
              }
              offset -= 75 * 0.05f;
              yield return new WaitForSeconds(0.05f);
         }
         transform.position = startPosition;
         if (transform.childCount > 0)
         {
              transform.GetChild(0).transform.position = firstChildPos;
         }
         redo = false;
    }
}
