using UnityEditor.TerrainTools;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public LayerMask deathLayerMask, edibleLayerMask, collidableLayerMask;
    public Vector3 boxSize;
    public Transform collisionCheckPoint;
    private Movement myMovement;
    private Spawner mySpawner;

    private void Start()
    {
        myMovement = GetComponent<Movement>();
        mySpawner = FindAnyObjectByType<Spawner>();
    }

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapBox(collisionCheckPoint.position, boxSize, transform.rotation, collidableLayerMask);

        foreach (Collider collider in hitColliders)
        {
            if ((edibleLayerMask.value & (1 << collider.gameObject.layer)) > 0)
            {
                myMovement.Grow();
                mySpawner.ReplaceEdible(collider.gameObject);
            }
            if ((deathLayerMask.value & (1 << collider.gameObject.layer)) > 0)
            {
                myMovement.Die();
            }
        }
    }
}
