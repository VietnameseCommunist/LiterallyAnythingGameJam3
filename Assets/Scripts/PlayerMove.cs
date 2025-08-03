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

    bool Grounded;

    public float JumpForce;
    public float DownForce;
    public float JumpDelay;


    Vector3 MoveDirection;

    Rigidbody RB;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.freezeRotation = true;
        Grounded = true;
    }

    private void Update()
    {
        MyInput();
        MovePlayer();

        if (Grounded != true)
        {
            Invoke("ReverseJump", JumpDelay);
        }


    }

    
    // Gets Inputs
    private void MyInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && Grounded == true)
        {
            
            Jump();
            Grounded = false;

        }

    }

    // Moves Player
    private void MovePlayer()
    {
        // Calculate Movement Direction
        MoveDirection = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;
        //transform.position += MoveDirection * Time.deltaTime * MoveSpeed;
        Vector3 targetPosition = RB.position + MoveDirection * Time.deltaTime * MoveSpeed;
        RB.MovePosition(targetPosition);
    }

    private void Jump()
    {
        RB.AddForce(0, JumpForce, 0, ForceMode.Impulse);
    }
    private void ReverseJump()
    {
        RB.AddForce(0, DownForce, 0, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Grounded = true;
        }
    }

}
