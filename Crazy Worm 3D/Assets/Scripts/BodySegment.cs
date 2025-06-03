using System.Collections.Generic;
using UnityEngine;

public class BodySegment : MonoBehaviour
{
    private Vector3 currentPosition, previousPosition;
    private int delay;
    private Queue<Vector3> positionHistory = new Queue<Vector3>();
    public Vector3 lastPositionSinceDelay { get; private set; }

    private void Start()
    {
        currentPosition = transform.position;
        delay = FindAnyObjectByType<Movement>().delay;
        lastPositionSinceDelay = transform.position;

        for (int i = 0; i < delay; i++)
        {
            positionHistory.Enqueue(lastPositionSinceDelay);
        }
    }

    private void FixedUpdate()
    {
        positionHistory.Enqueue(previousPosition);
        previousPosition = currentPosition;
        currentPosition = transform.position;

        if (positionHistory.Count > delay)
        {
            lastPositionSinceDelay = positionHistory.Dequeue();
        }
    }
}
