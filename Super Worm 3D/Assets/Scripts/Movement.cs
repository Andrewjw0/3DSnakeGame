using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    public float moveSpeed, steerSpeed, jumpForce, groundDistance;
    public int startingLength, delay;
    public GameObject bodySegmentPrefab;
    public LayerMask groundLayerMask;
    public Transform groundCheckPoint;
    public TextMeshProUGUI scoreText;
    public List<BodySegment> myBodySegments { get; private set; } = new List<BodySegment>();
    public Rigidbody myRigidbody { get; private set; }
    private int score = 1, highScore;
    private Death myDeath;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myBodySegments.Add(GetComponent<BodySegment>());
        myDeath = GetComponent<Death>();
        highScore = PlayerPrefs.GetInt("High Score");

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
    }

    private bool PlayerIsGrounded()
    {
        return Physics.CheckSphere(groundCheckPoint.position, groundDistance, groundLayerMask);
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
        transform.eulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
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

    public void Grow()
    {
        IncreaseScore();
        GameObject instantiatedBodySegment = Instantiate(bodySegmentPrefab, transform.position, transform.rotation);
        myBodySegments.Add(instantiatedBodySegment.GetComponent<BodySegment>());
    }

    private void IncreaseScore()
    {
        score++;

        if (score > highScore)
        {
            highScore = score;
        }

        scoreText.text = $"Score: {score}\nHigh Score: {highScore}";
    }

    public void Die()
    {
        Debug.Log("You Died!");

        if (highScore > PlayerPrefs.GetInt("High Score"))
        { 
            PlayerPrefs.SetInt("High Score", highScore);
        }

        myDeath.Die(); 
    }
}
