using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public Animator transition_UI;
    public float transitionTime = 0.8f;
    
    // not used, if used sceneIds need updating for christmas scene
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
        string currentScene = SceneManager.GetActiveScene().name;
        // if changing to christmas, turn down default music and play christmas music
        if (sceneName == "Christmas" && currentScene!="Christmas")
        {
            float timeElapsed = 0;
            transition.SetTrigger("Start");
            transition_UI.SetTrigger("Start");
            while (timeElapsed < (transitionTime / 2))
            {
                AudioManager.instance.christmasSource.Play();
                AudioManager.instance.musicSource.volume = Mathf.Lerp(1f, 0f, timeElapsed / transitionTime);
                AudioManager.instance.musicSource.volume = Mathf.Lerp(1f, 0f, timeElapsed / transitionTime);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            AudioManager.instance.musicSource.Stop();
            SceneManager.LoadScene(sceneName);
        }
        // if changing from christmas, turn down christmas music and play default music
        else if (currentScene=="Christmas" && sceneName!= "Christmas")
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
