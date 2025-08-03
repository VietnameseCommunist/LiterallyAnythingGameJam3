using UnityEngine;

public class Percentage : MonoBehaviour
{
     //Needle gameobject needed for rotation
     [SerializeField] GameObject NeedleGameObject;

     public static float value;
     public static float max;

     [SerializeField] float DebugValue;
     [SerializeField] float DebugMax;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         max = 1;
    }

    void FixedUpdate()
    {
         if (DebugValue == 0 && DebugMax == 0) return;
         value = DebugValue;
         max = DebugMax;
    }

    // Update is called once per frame
    void Update()
    {
         if (!NeedleGameObject)
         {
              return;
         }
         //To avoid useless division
         if (value == 0)
         {

              return;
         }
         //To avoid dividing by zero
         if (max == 0)
         {
              return;
         }
         NeedleGameObject.transform.rotation = Quaternion.Lerp(NeedleGameObject.transform.rotation, Quaternion.Euler(Mathf.Clamp(-180 + value / max * 180, -180, 0), 90,0), 3f * Time.deltaTime);
    }
}
