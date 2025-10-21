using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------AUDIO SOURCE------")]
    [SerializeField] AudioSource musicSource;


    [Header("------AUDIO CLIP------")]
    public AudioClip musicClip;

    public static AudioManager instance;

    public void Awake()
    {
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
        musicSource.clip = musicClip;
        musicSource.Play();
    }
}
