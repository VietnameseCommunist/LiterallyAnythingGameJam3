using UnityEngine;
using System.Collections;
public class InstantOverlay : MonoBehaviour
{
     [SerializeField] byte variant;
     [SerializeField] Color color;
     [SerializeField] float duration;
     [SerializeField] float fadingMultiplier;

     [SerializeField] bool slowsTime;
     [SerializeField] float timeChange;
     [SerializeField] float timeDuration;
     [SerializeField] float timeReturn = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
         yield return null;
         if (duration > 0)
               GradientOverlay.MakeTempGradient(variant, color, duration, fadingMultiplier);
          else
               GradientOverlay.MakeGradient(variant, color);
          if (!slowsTime) yield break;
          Time.timeScale = timeChange;
          yield return new WaitForSecondsRealtime(timeDuration);
          Time.timeScale = timeReturn;
    }
}
