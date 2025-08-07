using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class MarkerActivity : MonoBehaviour
{
    [SerializeField] float lerp;
    [SerializeField] Image img;
    [SerializeField] float fadingMultiplier;

    void Start()
    {
         StartCoroutine(Disappear());
    }
    // Update is called once per frame
    void Update()
    {
         transform.localScale = Vector3.LerpUnclamped(transform.localScale, Vector3.zero, lerp * Time.timeScale);
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
