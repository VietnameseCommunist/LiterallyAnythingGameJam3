using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gradient : MonoBehaviour
{
    public static Gradient Instance;
    public byte variant;

    private void Start()
    {
        if (Instance == null) Instance = this;
    }

    IEnumerator ColorToRedWhenDamageCoroutine()
    {
        Image seeyuh = Instance.gameObject.GetComponent<Image>();
        seeyuh.color = Color.red;
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            seeyuh.color = Color.Lerp(Color.red, Color.black, time);
            yield return null;
        }
    }
    public void ColorToRedWhenDamage() => StartCoroutine(ColorToRedWhenDamageCoroutine());
}
