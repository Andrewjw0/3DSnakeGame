using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public Transform regularCameraTransform, deathCameraTransform;
    public AnimationCurve positionCurve, rotationCurve;
    public float animationDuration, retryButtonDelay, cameraMoveDelay;
    public GameObject deathScreen, deathButtons;
    public AudioClip deathClip, loseClip;
    private Movement myMovement;
    private Rigidbody myRigidbody;
    private Collision myCollision;

    private void Start()
    {
        myMovement = GetComponent<Movement>();
        myRigidbody = GetComponent<Rigidbody>();
        myCollision = GetComponent<Collision>();
    }

    public void Die()
    {
        SoundManager.instance.Play(deathClip);
        myMovement.enabled = false;
        myRigidbody.isKinematic = true;
        myCollision.enabled = false;
        deathScreen.SetActive(true);
        StartCoroutine(MoveToDeathCameraLocation());
    }

    private IEnumerator MoveToDeathCameraLocation()
    {
        yield return new WaitForSeconds(cameraMoveDelay);
        SoundManager.instance.Play(loseClip);

        float time = 0;
        Vector3 startPos = regularCameraTransform.position;
        Vector3 endPos = deathCameraTransform.position;
        Quaternion startRot = regularCameraTransform.rotation;
        Quaternion endRot = deathCameraTransform.rotation;

        float start = Time.time;

        while (time < animationDuration)
        {
            float completion = time / animationDuration;
            regularCameraTransform.position = Vector3.Lerp(startPos, endPos, positionCurve.Evaluate(completion));
            regularCameraTransform.rotation = Quaternion.Lerp(startRot, endRot, rotationCurve.Evaluate(completion));
            time += Time.deltaTime;
            yield return null;
        }

        regularCameraTransform.position = endPos;
        regularCameraTransform.rotation = endRot;

        yield return new WaitForSeconds(retryButtonDelay);
        deathButtons.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
