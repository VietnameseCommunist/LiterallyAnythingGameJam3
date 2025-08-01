using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    // Variables
    public float MoveSpeed;

    public Transform Orientation;

    float HorizontalInput;
    float VerticalInput;

    Vector3 MoveDirection;

    Rigidbody RB;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        MovePlayer();
    }

    
    // Gets Inputs
    private void MyInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }

    // Moves Player
    private void MovePlayer()
    {
        
        // Calculate Movement Direction
        MoveDirection = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;
        transform.position += MoveDirection * Time.deltaTime * MoveSpeed;
    }
}
