using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public Animator transition_UI;
    public float transitionTime = 0.8f;

   public IEnumerator LoadSceneById(int sceneId)
    {
        transition.SetTrigger("Start");
        transition_UI.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneId);
    }

    public IEnumerator LoadSceneByName(string sceneName)
    {
        transition.SetTrigger("Start");
        transition_UI.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    } 
}
