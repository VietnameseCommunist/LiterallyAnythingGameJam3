using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class MarkerActivity : MonoBehaviour
{
    [SerializeField] float lerp;
    [SerializeField] Image img;
    [SerializeField] float fadingMultiplier;
    [SerializeField] float tempNegative;
    bool isNegative;
    void Start()
    {
         StartCoroutine(Disappear());
    }
    // Update is called once per frame
    void Update()
    {
         transform.localScale = Vector3.LerpUnclamped(transform.localScale, Vector3.zero, tempNegative > 0 || isNegative && tempNegative == 0 ? lerp * -Time.timeScale : lerp * Time.timeScale);
         if (tempNegative > 0)
         {
              isNegative = false;
              if (tempNegative - Time.deltaTime > 0) tempNegative -= Time.deltaTime;
              else tempNegative = 0;
         }
         if (tempNegative < 0)
         {
              isNegative = true;
              if (tempNegative + Time.deltaTime < 0) tempNegative += Time.deltaTime;
              else tempNegative = 0;
         }
    }

    IEnumerator Disappear()
    {
         while (img.color.a >= 0.1f)
         {
              img.color -= new Color(0, 0, 0, 25 * fadingMultiplier * Time.deltaTime);
              yield return null;
         }
         Destroy(this.gameObject);
    }
}
