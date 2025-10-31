using UnityEngine;

public class CoinMoveScript : MonoBehaviour
{
    private float moveSpeed = 5f;
    public float deadZone = -45;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("pipe deleted");
            Destroy(gameObject);
        }
    }

    public void setMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
    public float getMoveSpeed()
    {
        return moveSpeed;
    }
}
