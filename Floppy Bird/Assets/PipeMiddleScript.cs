using UnityEngine;


public class PipeMiddleScript : MonoBehaviour
{
    public UnderWater fish;
    public BirdScript bird;
    public LogicScript logic;
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
                logic.addScore(1);
            }
        }

        if (fish != null)
        {
            if (collision.gameObject.layer == 3 && fish.birdIsAlive)
            {
                logic.addScore(1);
            }
        }

    }
}
