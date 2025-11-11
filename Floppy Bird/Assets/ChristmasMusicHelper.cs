using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;
using System.Collections;


public class ChristmasMusicHelper : MonoBehaviour
{
    public float transitionTime = 0.4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("playing christmas music");
        StartCoroutine(StartChristmasMusic());
    }

    public IEnumerator StartChristmasMusic()
    {
        {
            float timeElapsed = 0;
            while (timeElapsed < transitionTime)
            {
                AudioManager.instance.christmasSource.Play();
                AudioManager.instance.musicSource.volume = Mathf.Lerp(1f, 0f, timeElapsed / transitionTime);
                timeElapsed += Time.deltaTime;
                yield return null;
                
            }
        }

    }
}
