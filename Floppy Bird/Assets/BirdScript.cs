using UnityEngine;

public class BirdScript : MonoBehaviour
{

    public Rigidbody2D myRigidbody2d;
    public float flapStrength;
    public float halfFlapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        halfFlapStrength = 0.5f * flapStrength;
        myRigidbody2d.linearVelocity = Vector2.up * (flapStrength + halfFlapStrength);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidbody2d.linearVelocity = Vector2.up * flapStrength;
        }
        if (transform.position.y > 17 || transform.position.y < -17)
        {
            deadBird();
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        deadBird();
    }

    private void deadBird()
    {
        if (birdIsAlive)
        {
            audioManager.PlaySFX(audioManager.failClip);
            myRigidbody2d.linearVelocity = Vector2.up * flapStrength * 0.70f;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
            
            // stops collisions with pipes
            GetComponent<Collider2D>().enabled = false;
            myRigidbody2d.AddTorque(20f);
        }
        birdIsAlive = false;
        logic.gameOver();
        StartCoroutine(StopAfterFall());
    }

    private System.Collections.IEnumerator StopAfterFall()
    {
        yield return new WaitForSeconds(1.5f);
        myRigidbody2d.simulated = false;
    }

}
