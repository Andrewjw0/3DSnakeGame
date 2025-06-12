using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public float animationDuration;
    public Transform screenHolder;
    public AnimationCurve myAnimationCurve;
    public int nextSceneIndex, levelCount;
    public AudioClip clickClip;

    public void ChangeScreenAndScene(bool up)
    {
        float distance = up ? -450 : 450;
        StartCoroutine(MoveScreenLocation(distance, true, nextSceneIndex));
    }

    public void ChangeScreen(bool up)
    {
        float distance = up ? -450 : 450;
        StartCoroutine(MoveScreenLocation(distance, false));
    }

    private IEnumerator MoveScreenLocation(float distance, bool changeScene, int nextSceneIndex = 0)
    {
        float time = 0;
        Vector3 startPos = screenHolder.localPosition;
        Vector3 endPos = new Vector3(0, screenHolder.localPosition.y + distance, 0);

        float start = Time.time;

        while (time < animationDuration)
        {
            float completion = time / animationDuration;
            screenHolder.localPosition = Vector3.Lerp(startPos, endPos, myAnimationCurve.Evaluate(completion));
            time += Time.deltaTime;
            yield return null;
        }

        screenHolder.localPosition = endPos;

        yield return new WaitForEndOfFrame();

        if (changeScene)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level + 1);
    }

    public void SetHighScoreCounter(TextMeshProUGUI counter)
    {
        string counterText = "";

        for (int i = 1; i <= levelCount; i++)
        {
            counterText += $"High Score: {PlayerPrefs.GetInt($"High Score {i}")}\n";
        }

        counter.text = counterText;
    }

    public void PlayAudio()
    {
        SoundManager.instance.Play(clickClip);
    }
}
