using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed, steerSpeed, jumpForce, groundDistance;
    public int startingLength, delay;
    public GameObject bodySegmentPrefab;
    public LayerMask groundLayer;
    public Transform groundCheckPoint;
    private Rigidbody myRigidbody;
    private List<BodySegment> myBodySegments = new List<BodySegment>();


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myBodySegments.Add(GetComponent<BodySegment>());

        for (int i = 0; i < startingLength - 1; i++)
        {
            Grow();
        }
    }

    private void Update()
    {
        if (PlayerIsGrounded() && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Grow();
        }
    }

    private bool PlayerIsGrounded()
    {
        // OnDrawGizmos();
        return Physics.CheckSphere(groundCheckPoint.position, groundDistance, groundLayer);
    }


    private void FixedUpdate()
    {
        UpdateBodySegments();
        Move();
    }

    private void Jump()
    {
        myRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Move()
    {
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.fixedDeltaTime);

        Vector3 horizontalMovement = transform.forward * moveSpeed * Time.fixedDeltaTime;
        myRigidbody.MovePosition(transform.position + horizontalMovement);
    }

    private void UpdateBodySegments()
    {
        for (int i = 1; i < myBodySegments.Count; i++)
        {
            BodySegment currentSegment = myBodySegments[i];
            BodySegment previousSegment = myBodySegments[i - 1];

            currentSegment.transform.LookAt(previousSegment.lastPositionSinceDelay);
            currentSegment.transform.position = previousSegment.lastPositionSinceDelay;
        }
    }

    private void Grow()
    {
        GameObject instantiatedBodySegment = Instantiate(bodySegmentPrefab, transform.position, transform.rotation);
        myBodySegments.Add(instantiatedBodySegment.GetComponent<BodySegment>());
    }
}
