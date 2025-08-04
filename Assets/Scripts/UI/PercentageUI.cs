using UnityEngine;

public class Percentage : MonoBehaviour
{
     //Needle gameobject needed for rotation
     [SerializeField] GameObject NeedleGameObject;

     public static float value;
     public static float max;
     public static bool instant;

     [SerializeField] float DebugValue;
     [SerializeField] float DebugMax;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         max = 1;
         instant = true;
    }

    void FixedUpdate()
    {
         if (DebugValue == 0 && DebugMax == 0) return;
         value = DebugValue;
         max = DebugMax;
    }

    // Update is called once per frame
    void LateUpdate()
    {
         if (!NeedleGameObject)
         {
              return;
         }
         //To avoid useless division
         if (value == 0)
         {
              if (!instant) NeedleGameObject.transform.localRotation = Quaternion.Lerp(NeedleGameObject.transform.localRotation, Quaternion.Euler(-Mathf.Clamp(0, 0, 180), 0, 0), 3f * Time.deltaTime);
              else NeedleGameObject.transform.localRotation = Quaternion.Euler(-Mathf.Clamp(0, 0, 180),0,0);
              return;
         }
         //To avoid dividing by zero
         if (max == 0)
         {
              return;
         }
         if (!instant) NeedleGameObject.transform.localRotation = Quaternion.Lerp(NeedleGameObject.transform.localRotation, Quaternion.Euler(-Mathf.Clamp(value / max * 180, 0, 180), 0, 0), 3f * Time.deltaTime);
         else NeedleGameObject.transform.localRotation = Quaternion.Euler(-Mathf.Clamp(value / max * 180, 0, 180),0,0);
    }
}
