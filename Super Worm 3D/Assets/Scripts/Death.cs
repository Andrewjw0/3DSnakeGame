using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public Transform regularCameraTransform, deathCameraTransform;
    public AnimationCurve curve;
    public float duration;
    public GameObject deathScreen;
    private Movement myMovement;

    private void Start()
    {
        myMovement = GetComponent<Movement>();
    }

/*    public void Die()
    {
        myMovement.enabled = false;
        myMovement.myRigidbody.isKinematic = true;
        deathScreen.SetActive(true);
        StartCoroutine(MoveToDeathCameraLocation(duration));
    }

    private IEnumerator MoveToDeathCameraLocation(float time)
    {
        Vector3 startPos = regularCameraTransform.position;
        Vector3 endPos = deathCameraTransform.position;
        Quaternion startRot = regularCameraTransform.rotation;
        Quaternion endRot = deathCameraTransform.rotation;

        float start = Time.time;

        while (Time.time < start + time)
        {
            float completion = (Time.time - start) / time;
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(completion));
            transform.rotation = Quaternion.Lerp(startRot, endRot, curve.Evaluate(completion));
            yield return null;
        }

        transform.position = endPos;
        transform.rotation = endRot;
    }*/

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
