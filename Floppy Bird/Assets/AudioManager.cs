using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------AUDIO SOURCE------")]
    public AudioSource musicSource;
    public AudioSource christmasSource;
    [SerializeField] AudioSource soundFX;


    [Header("------AUDIO CLIP------")]
    public AudioClip musicClip;
    public AudioClip christmasClip;
    public AudioClip coinClip;
    public AudioClip flapClip;
    public AudioClip swipeClip;
    public AudioClip failClip;
    public AudioClip selectClip;

    public bool isPlayingChristmas;

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
        christmasSource.clip = christmasClip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        soundFX.PlayOneShot(clip);
    }
}
