using UnityEngine;

public class UnderWater : MonoBehaviour
{
    [Header("Underwater Physics")]
    public float maxDepth;
    public float buoyancyMultiplier;
    public float diveForce;

    private float currentDepth;
    private Rigidbody2D myRigidbody2d;
    public float singleDive;
    public float halfDive;
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
        myRigidbody2d = GetComponent<Rigidbody2D>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
        currentDepth = Mathf.Max(0, 12 - transform.position.y);

        float buoyancyForce = (currentDepth / maxDepth) * buoyancyMultiplier;

        if (birdIsAlive)
        {
            myRigidbody2d.AddForce(Vector2.up * buoyancyForce);
            if (Input.GetKey(KeyCode.Space))
            {
                myRigidbody2d.AddForce(Vector2.down * diveForce);
            }
        }
        else
        { 
            myRigidbody2d.AddForce(Vector2.up * buoyancyForce * 0.5f);
        }
        if (transform.position.y > 12 || transform.position.y < -12)
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
            myRigidbody2d.linearVelocity = Vector2.down * singleDive * 0.70f;
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
        yield return new WaitForSeconds(3.5f);
        myRigidbody2d.simulated = false;
    }
}
