using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------AUDIO SOURCE------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundFX;


    [Header("------AUDIO CLIP------")]
    public AudioClip musicClip;
    public AudioClip coinClip;
    public AudioClip flapClip;
    public AudioClip swipeClip;
    public AudioClip failClip;
    public AudioClip selectClip;
    


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

    public void PlaySFX(AudioClip clip)
    {
        soundFX.PlayOneShot(clip);
    }
}
