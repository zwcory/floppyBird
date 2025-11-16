using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public ParticleManager particleManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particleManager = GameObject.FindGameObjectWithTag("ParticleManager").GetComponent<ParticleManager>();
        particleManager.DestroyParticleManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
