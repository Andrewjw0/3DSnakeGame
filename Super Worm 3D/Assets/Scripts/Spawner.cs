using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class Spawner : MonoBehaviour
{
    public float minX, maxX, minY, maxY, minDistance;
    public GameObject apple, spike;
    public int startingAmountOfApples, obstaclesToSpawnAfterConsuming;
    public List<GameObject> prePlacedObstacles;
    private List<GameObject> spawnedEdibles = new List<GameObject>();
    private List<GameObject> spawnedObstacles;
    private float sqrMinDistance;
    private Movement myMovement;

    private void Start()
    {
        myMovement = FindAnyObjectByType<Movement>();
        sqrMinDistance = minDistance * minDistance;
        spawnedObstacles = prePlacedObstacles;

        for (int i = 0; i < startingAmountOfApples; i++)
        {
            SpawnEdible();
        }
    }

    private void SpawnEdible()
    {
        List<BodySegment> segments = myMovement.myBodySegments;
        Vector3 randomLocation = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minY, maxY));
        bool overlaps = false;

        foreach (BodySegment segment in segments)
        {
            if (!overlaps && (segment.transform.position - randomLocation).sqrMagnitude < sqrMinDistance)
            {
                overlaps = true;
                Debug.Log("Overlaps Segment!");
                SpawnEdible();
            }
        }

        if (!overlaps)
        {
            foreach (GameObject edible in spawnedEdibles)
            {
                if (!overlaps && (edible.transform.position - randomLocation).sqrMagnitude < sqrMinDistance)
                {
                    overlaps = true;
                    Debug.Log("Overlaps Edible!");
                    SpawnEdible();
                }
            }
        }

        if (!overlaps)
        {
            foreach (GameObject obstacle in spawnedObstacles)
            {
                if (!overlaps && (obstacle.transform.position - randomLocation).sqrMagnitude < sqrMinDistance)
                {
                    overlaps = true;
                    Debug.Log("Overlaps Obstacle!");
                    SpawnEdible();
                }
            }
        }

        if (!overlaps)
        {
            spawnedEdibles.Add(Instantiate(apple, randomLocation, Quaternion.identity));
        }
    }

    private void SpawnObstacle()
    {
        List<BodySegment> segments = myMovement.myBodySegments;
        Vector3 randomLocation = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minY, maxY));
        bool overlaps = false;

        if ((myMovement.transform.forward - randomLocation).sqrMagnitude < sqrMinDistance)
        {
            overlaps = true;
            Debug.Log("Obstacle in front of player!");
            SpawnObstacle();
        }

        if (!overlaps)
        {
            foreach (BodySegment segment in segments)
            {
                if (!overlaps && (segment.transform.position - randomLocation).sqrMagnitude < sqrMinDistance)
                {
                    overlaps = true;
                    Debug.Log("OoS!");
                    SpawnObstacle();
                }
            }
        }

        if (!overlaps)
        {
            foreach (GameObject edible in spawnedEdibles)
            {
                if (!overlaps && (edible.transform.position - randomLocation).sqrMagnitude < sqrMinDistance)
                {
                    overlaps = true;
                    Debug.Log("OoE!");
                    SpawnObstacle();
                }
            }
        }

        if (!overlaps)
        {
            foreach (GameObject obstacle in spawnedObstacles)
            {
                if (!overlaps && (obstacle.transform.position - randomLocation).sqrMagnitude < sqrMinDistance)
                {
                    overlaps = true;
                    Debug.Log("OoO!");
                    SpawnObstacle();
                }
            }
        }

        if (!overlaps)
        {
            spawnedEdibles.Add(Instantiate(spike, randomLocation, Quaternion.identity));
        }
    }

    public void ReplaceEdible(GameObject edibleToReplace)
    {
        spawnedEdibles.Remove(edibleToReplace);
        Destroy(edibleToReplace);
        SpawnEdible();

        for (int i = 0; i < obstaclesToSpawnAfterConsuming; i++)
        {
            SpawnObstacle();
        }
    }
}
