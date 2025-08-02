using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
     //this is for the health bar Image Component, this will be accessed for fillAmount
     public Image img;
     //For all other HealthUI access
     public static HealthUI Instance;
     //This makes the change
     public static void SetTo(float value, float max)
     {
          if (!Instance)
          {
               Debug.Log("No Instance in HealthUI");
               return;
          }
          if (Instance.img)
          {
               //To avoid dividing by zero
               if (max == 0)
               {
                    Instance.img.fillAmount = 1;
                    return;
               }
               //To avoid useless division
               if (value == 0)
               {
                    Instance.img.fillAmount = 0;
                    return;
               }
               Instance.img.fillAmount = value / max;
          }
     }
}
