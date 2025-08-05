using TMPro;
using UnityEngine;

public class TestCanvas : MonoBehaviour
{
    public TextMeshProUGUI HoldingState;
    public TextMeshProUGUI Bullets;

    public static string BulletsString;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bullets.gameObject.SetActive(false);

    BulletsString = "10/10";
    }

    // Update is called once per frame
    void Update()
    {
        HoldingState.text = PlayerScript.instance.HoldState.ToString();
        Bullets.text = BulletsString;

        if (PlayerScript.instance.HoldState == PlayerScript.HoldingState.Holding && PlayerScript.instance.HoldingObject.GetComponent<Gun>()) Bullets.gameObject.SetActive(true);
        else Bullets.gameObject.SetActive(false);
    }
}
