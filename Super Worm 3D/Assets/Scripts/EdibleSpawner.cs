using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class EdibleSpawner : MonoBehaviour
{
    public float minX, maxX, minY, maxY, minDistance;
    public GameObject apple;
    public int startingAmount;
    private List<GameObject> spawnedEdibles = new List<GameObject>();
    private float sqrMinDistance;
    private Movement myMovement;

    private void Start()
    {
        myMovement = FindAnyObjectByType<Movement>();
        sqrMinDistance = minDistance * minDistance;

        for (int i = 0; i < startingAmount; i++)
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
            spawnedEdibles.Add(Instantiate(apple, randomLocation, Quaternion.identity));
        }
    }

    public void ReplaceEdible(GameObject edibleToReplace)
    {
        spawnedEdibles.Remove(edibleToReplace);
        Destroy(edibleToReplace);
        SpawnEdible();
    }
}
