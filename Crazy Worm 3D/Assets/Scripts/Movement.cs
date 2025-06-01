using UnityEditor.U2D;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed, steerSpeed, jumpForce, gravity;
    private CharacterController myCharacterController;
    private float jumpVelocity;

    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Jump()
    {
        if (myCharacterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumpVelocity = jumpForce;
            }
        }
        else
        {
            jumpVelocity -= gravity * Time.deltaTime;
        }
    }

    private void Move()
    {
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.deltaTime);

        Jump();

        Vector3 horizontalMovement = transform.forward * moveSpeed * Time.deltaTime;
        myCharacterController.Move(horizontalMovement + Vector3.up * jumpVelocity);
    }
}
