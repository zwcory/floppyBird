using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    // Kept public because setSpawnRate doesnt work when spawnRate is set to private (??)

    public GameObject pipe;
    public float spawnRate = 3f;
    private float timer = 0;
    public float heightOffset;
    public GameObject coin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        } else
        {
            spawnPipe();
            timer = 0;
        }

    }
    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        float randomSpawn = Random.Range(lowestPoint, highestPoint);
        Instantiate(pipe, new Vector3(transform.position.x, randomSpawn , 0), transform.rotation);
        
        int randomNumber = Random.Range(1, 6);
        if (randomNumber == 1)
        {
            Instantiate(coin, new Vector3(transform.position.x, randomSpawn, 0), transform.rotation);
        }
    }

    public void setSpawnRate(float speed)
    {
        spawnRate = speed;
    }
    public float getSpawnRate()
    {
        return spawnRate;
    }
}
