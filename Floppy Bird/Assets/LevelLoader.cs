using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public Animator transition_UI;
    public float transitionTime = 0.8f;
    
   public IEnumerator LoadSceneById(int sceneId)
    {
        if (sceneId == 4)
        {
            float timeElapsed = 0;
            transition.SetTrigger("Start");
            transition_UI.SetTrigger("Start");
            while (timeElapsed < (transitionTime / 2))
            {
                AudioManager.instance.musicSource.volume = Mathf.Lerp(1f, 0f, timeElapsed / transitionTime);
                yield return null;
                timeElapsed += Time.deltaTime;
            }
            AudioManager.instance.musicSource.Stop();
            SceneManager.LoadScene(sceneId);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            float timeElapsed = 0;
            transition.SetTrigger("Start");
            transition_UI.SetTrigger("Start");
            while (timeElapsed < (transitionTime / 2))
            {
                AudioManager.instance.christmasSource.volume = Mathf.Lerp(1f, 0f, timeElapsed / transitionTime);
                AudioManager.instance.musicSource.volume = Mathf.Lerp(0f, 1f, timeElapsed / transitionTime);
                timeElapsed += Time.deltaTime;
                yield return null;

            }
            AudioManager.instance.christmasSource.Stop();
            SceneManager.LoadScene(sceneId);
        } else
        {
            transition.SetTrigger("Start");
            transition_UI.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(sceneId);
        }
        
    }

    public IEnumerator LoadSceneByName(string sceneName)
    {
        if (sceneName == "Christmas")
        {
            float timeElapsed = 0;
            transition.SetTrigger("Start");
            transition_UI.SetTrigger("Start");
            while (timeElapsed < (transitionTime / 2))
            {
                AudioManager.instance.musicSource.volume = Mathf.Lerp(1f, 0f, timeElapsed / transitionTime);
                timeElapsed += Time.deltaTime;
                yield return null;

            }
            AudioManager.instance.christmasSource.Stop();
            SceneManager.LoadScene(sceneName);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            float timeElapsed = 0;
            transition.SetTrigger("Start");
            transition_UI.SetTrigger("Start");
            while (timeElapsed < (transitionTime/2))
            {
                AudioManager.instance.christmasSource.volume = Mathf.Lerp(1f, 0f, timeElapsed / transitionTime);
                AudioManager.instance.musicSource.volume = Mathf.Lerp(0f, 1f, timeElapsed / transitionTime);
                timeElapsed += Time.deltaTime;
                yield return null;

            }
            AudioManager.instance.christmasSource.Stop();
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            transition.SetTrigger("Start");
            transition_UI.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(sceneName);
        }
    } 
}
