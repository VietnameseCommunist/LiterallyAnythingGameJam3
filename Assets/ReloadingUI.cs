using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ReloadingUI : MonoBehaviour
{
     public static ReloadingUI Instance;

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
              Instance.StartCoroutine(Instance.Reload(time));
         }
    }

    public IEnumerator Reload(float time)
    {
         float elapsedTime = 0;
         while (time > elapsedTime)
         {
              yield return null;
              elapsedTime += Time.deltaTime;
              Debug.Log(elapsedTime);
              if (img) img.fillAmount = Mathf.Lerp(0, 1, elapsedTime / time);
         }
         img.fillAmount = 1;
         float animation = 1f;
         while (animation < 1.4f)
         {
              transform.localScale = Vector3.one * animation;
              animation += Time.deltaTime * transform.localScale.magnitude;
              yield return null;
         }
         while (animation > 0)
         {
              transform.localScale = Vector3.one * animation;
              animation -= Time.deltaTime / (transform.localScale.magnitude / 2);
              yield return null;
         }
         transform.localScale = Vector3.one;
         gameObject.SetActive(false);
    }
}
