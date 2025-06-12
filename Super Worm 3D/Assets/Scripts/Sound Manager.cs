using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource myAudioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        myAudioSource.PlayOneShot(clip);
    }
}