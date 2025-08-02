using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject HoldingObject;
    public bool IsHolding;
    public bool IsGun;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         GradientOverlay.MakeGradient(0, Color.black);
        IsHolding = false;
        IsGun = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
