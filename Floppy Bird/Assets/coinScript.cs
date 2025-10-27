using UnityEngine;

public class coinScript : MonoBehaviour
{
    public BirdScript bird;
    public LogicScript logic;
    public Animator transition;
    public float moveSpeed;
    public float deadZone = -45;
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
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("coin deleted");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && bird.birdIsAlive)
        {
            moveSpeed = 0;
            int randomNumber = Random.Range(1, 4);
            logic.addCoin(randomNumber);
            audioManager.PlaySFX(audioManager.coinClip);
            Debug.Log("added " + randomNumber + " coins");
            transition.SetTrigger("Collected");
            
        }

    }
}
