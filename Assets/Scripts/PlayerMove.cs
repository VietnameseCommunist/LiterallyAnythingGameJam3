using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed;

    public Transform Orientation;

    float HorizontalInput;
    float VerticalInput;

    // KeyCode 
    public KeyCode jumpKey = KeyCode.Space;

    bool ReadyToJump;
    bool Grounded;

    public float JumpForce;

    Vector3 MoveDirection;

    Rigidbody RB;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.freezeRotation = true;
        ReadyToJump = true;
    }

    private void Update()
    {
        MyInput();
        MovePlayer();

        if(Grounded == true)
        {
            ReadyToJump = true;
        }


    }

    
    // Gets Inputs
    private void MyInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && ReadyToJump == true && Grounded != true)
        {
            ReadyToJump = false;
            Jump();

        }

    }

    // Moves Player
    private void MovePlayer()
    {
        
        // Calculate Movement Direction
        MoveDirection = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;
        transform.position += MoveDirection * Time.deltaTime * MoveSpeed;

    }

    private void Jump()
    {
        ReadyToJump = false;
        RB.AddForce(0, JumpForce, 0, ForceMode.Impulse);
        ReadyToJump = true;
    }

}
