using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ReloadingUI : MonoBehaviour
{
     public static ReloadingUI Instance;

     public static bool interruptus;

     public Image img;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         if (!Instance) Instance = this;
    }

    public static void StartReload(float time)
    {
         if (Instance)
         {
              if (!Instance.gameObject.activeSelf)
              {
                   Instance.gameObject.SetActive(true);
              }
              interruptus = true;
              Instance.StartCoroutine(Instance.Reload(time));
         }
    }

    public IEnumerator Reload(float time)
    {
         yield return null;
         if (interruptus)
         {
              interruptus = false;
         }
         float elapsedTime = 0;
         while (time > elapsedTime)
         {
              yield return null;
              elapsedTime += Time.deltaTime;
              Debug.Log(elapsedTime);
              if (interruptus)
              {
                   yield break;
              }
              if (img) img.fillAmount = Mathf.Lerp(0, 1, elapsedTime / time);
         }
         img.fillAmount = 1;
         float animation = 1f;
         while (animation < 1.4f)
         {
              transform.localScale = Vector3.one * animation;
              animation += Time.deltaTime * transform.localScale.magnitude;
              if (interruptus)
              {
                   transform.localScale = Vector3.one;
                   yield break;
              }
              yield return null;
         }
         while (animation > 0)
         {
              transform.localScale = Vector3.one * animation;
              animation -= Time.deltaTime / (transform.localScale.magnitude / 2);
              if (interruptus)
              {
                   transform.localScale = Vector3.one;
                   yield break;
              }
              yield return null;
         }
         DisableThis();
    }

    void DisableThis()
    {
         transform.localScale = Vector3.one;
         gameObject.SetActive(false);
    }
}
