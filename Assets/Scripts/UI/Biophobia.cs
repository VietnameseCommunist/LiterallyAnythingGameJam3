using UnityEngine;
using UnityEngine.UI;
public class Biophobia : MonoBehaviour
{
     public static Biophobia Instance;
     public Sprite[] sprites;
     public Image img;
     void Start()
     {
          if (!Instance) Instance = this;
     }

    // Update is called once per frame
    public static void SwitchSprite()
    {
         if (!Instance) return;
         if (Instance.sprites == null || Instance.sprites.Length <= 1) return;
         if (!Instance.img) return;
         Debug.Log("requirement check passed");
         if (Instance.img.sprite == Instance.sprites[0])
               Instance.img.sprite = Instance.sprites[1];
         else if (Instance.img.sprite == Instance.sprites[1])
               Instance.img.sprite = Instance.sprites[0];
    }
}
