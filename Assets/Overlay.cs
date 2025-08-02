using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GradientOverlay : MonoBehaviour
{
     public static GradientOverlay Instance;
     public Gradient[] gradients;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         if (!Instance) Instance = this;
         StartCoroutine(GradientTemporary(2, Color.black, 5, 1));
    }

    // Update is called once per frame
    public static Gradient MakeGradient(byte variant, Color color)
    {
         if (Instance)
         foreach (Gradient grad in Instance.gradients)
         {
              if (!grad) continue;
              if (grad.variant == variant && grad.gameObject.GetComponent<Image>())
              {
                   GameObject instantiated = Instantiate(grad.gameObject);
                   instantiated.transform.SetParent(Instance.transform.root);
                   instantiated.GetComponent<RectTransform>().offsetMax = new Vector2(0,0);
                   instantiated.GetComponent<RectTransform>().offsetMin = new Vector2(0,0);
                   instantiated.GetComponent<Image>().color = color;
                   return instantiated.GetComponent<Gradient>();
              }
         }
         Debug.Log("Gradients issue: Instance is none");
         return null;
    }
    //For Temporary gradients
    public static IEnumerator GradientTemporary(byte variant, Color color,float duration, float fadingMultiplier = 1)
    {
         Gradient real = MakeGradient(variant, color);
         yield return new WaitForSeconds(duration);
          if (real.gameObject.GetComponent<Image>())
          {
               Image img = real.gameObject.GetComponent<Image>();
               while (img.color.a >= 0)
               {
                    img.color -= new Color(0, 0, 0, 5 * fadingMultiplier * Time.deltaTime);
                    yield return null;
               }
               Destroy(real.gameObject);
          }
    }

    public static void MakeTempGradient(byte variant, Color color,float duration, float fadingMultiplier = 1)
    {
         if (Instance) Instance.StartCoroutine(GradientOverlay.GradientTemporary(variant, color, duration, fadingMultiplier));
    }
}
