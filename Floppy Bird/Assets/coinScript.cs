using UnityEngine;

public class coinScript : MonoBehaviour
{
    public BirdScript bird;
    public UnderWater fish;
    public LogicScript logic;
    public Animator transition;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        bird = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>();
        fish = GameObject.FindGameObjectWithTag("Bird").GetComponent<UnderWater>();
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bird != null)
        {
            if (collision.gameObject.layer == 3 && bird.birdIsAlive)
            {
                GetComponent<Collider2D>().enabled = false;
                int randomNumber = Random.Range(1, 4);
                logic.addCoin(randomNumber);
                audioManager.PlaySFX(audioManager.coinClip);
                Debug.Log("added " + randomNumber + " coins");
                transition.SetTrigger("Collected");

            }
        } else if (fish != null)
        {
            if (collision.gameObject.layer == 3 && fish.birdIsAlive)
            {
                GetComponent<Collider2D>().enabled = false;
                int randomNumber = Random.Range(1, 4);
                logic.addCoin(randomNumber);
                audioManager.PlaySFX(audioManager.coinClip);
                Debug.Log("added " + randomNumber + " coins");
                transition.SetTrigger("Collected");

            }
        }
    }
}
