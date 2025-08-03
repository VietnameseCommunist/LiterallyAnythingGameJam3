using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    //variables
    public float SensX;
    public float SensY;

    public Transform orientation;

    float XRotation;
    float YRotation;

    public static float DefaultPOV = 70;
    public Camera cam;

    public void Start()
    {
       // Locks Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cam = GetComponent<Camera>();
        cam.fieldOfView = DefaultPOV;
    }

    public void Update()
    {
        //Gets Mouse Inputs
        float MouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
        float MouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;

        YRotation += MouseX;
        XRotation -= MouseY;

        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        //Rotate Cam and Orientation
        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
        orientation.rotation = Quaternion.Euler(0, YRotation, 0);


    }

}
