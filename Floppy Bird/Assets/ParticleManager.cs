using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public static ParticleManager instance;
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

    public void DestroyParticleManager()
    {
        Destroy(gameObject);
    }
}
