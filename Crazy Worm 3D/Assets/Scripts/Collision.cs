using UnityEngine;

public class Collision : MonoBehaviour
{
    public LayerMask deathLayerMask, edibleLayerMask;
    public float raycastLength;
    private Movement myMovement;
    private int deathLayerInt, edibleLayerInt;

    private void Start()
    {
        myMovement = GetComponent<Movement>();
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if ((edibleLayerMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            Destroy(collision.gameObject);
            transform.eulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            myMovement.Grow();
        }
        if ((deathLayerMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            myMovement.Die();
        }
    }
}
