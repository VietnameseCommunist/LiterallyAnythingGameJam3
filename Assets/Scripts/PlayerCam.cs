using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public static PlayerCam Instance;

    //variables
    public float SensX;
    public float SensY;

    public Transform orientation;

    float XRotation;
    float YRotation;

    public static float DefaultPOV = 70;
    public Camera cam;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
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
    public void XrotationChange(int changes)
    {
        XRotation += -changes;
    }

}
