using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class AmmunitionUI : MonoBehaviour
{
     public static AmmunitionUI Instance;
     public static bool redo;

     public Vector3 startPosition;
     public Vector3 firstChildPos;

     public Sprite defaulter;
     public Sprite[] ammoSprites;
     public Image img;

     public Text AmmoCount;

     [SerializeField] float debugRecoil;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         if (!Instance) Instance = this;
         startPosition = transform.position;
         firstChildPos = transform.GetChild(0).transform.localPosition;
    }

    void Update()
    {
         if (Input.GetKeyDown(KeyCode.P))
         {
              ShootEffect(debugRecoil);
         }

         if (img.sprite == null && defaulter)
         {
              img.sprite = defaulter;
         }
    }

    // Update is called once per frame
    public static void ShootEffect(float recoil)
    {
         if (Instance && recoil > 0) Instance.StartCoroutine(Instance.Recoil(recoil));
    }

    public static int ChangeAmmoCount(int number, int max)
    {
         if (Instance)
         {
              if (Instance.AmmoCount)
              {
                  Instance.AmmoCount.text = number.ToString() + "/" + max.ToString();
              }
         }
         return number;
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
              transform.GetChild(0).transform.localPosition = firstChildPos;
         }
         float goThrough = recoil;
         float offset = 0;
         float modifier = Mathf.Clamp(recoil, 100f, 500f);
         redo = false;
         yield return null;
         redo = true;

         bool temp = false;
         while (goThrough > 0)
         {
              if (!redo)
              {
                   redo = true;
                   yield break;
              }
              goThrough -= modifier * Time.deltaTime * 5;
              if (offset < 100)
              {
                   offset += modifier * Time.deltaTime * 5;
                   transform.position += new Vector3(modifier * Time.deltaTime * 5, 0, 0);
                   if (transform.childCount > 0)
                   {
                        transform.GetChild(0).transform.localPosition += new Vector3(modifier * Time.deltaTime * 5, 0, 0) * 1.5f;
                   }
              }
              else
              {
                   if (!temp)
                   {
                        goThrough = (offset - 60) / 3;
                        temp = true;
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
              transform.position = Vector3.Lerp(transform.position, startPosition, modifier * Time.deltaTime / 30);
              if (transform.childCount > 0)
              {
                   transform.GetChild(0).transform.localPosition = Vector3.Lerp(transform.GetChild(0).transform.localPosition, firstChildPos, modifier * Time.deltaTime / 30);
              }
              offset -= (offset + 10) * Time.deltaTime;
              yield return null;
         }
         transform.position = startPosition;
         if (transform.childCount > 0)
         {
              transform.GetChild(0).transform.localPosition = firstChildPos;
         }
         redo = false;
    }
}
