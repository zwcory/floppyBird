using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleLogic : MonoBehaviour
{
    
    public void startGame()
    {
        //SceneManager.LoadScene(SceneManager.GetSceneByName("SampleScene").name);
        SceneManager.LoadScene("MainGame");
    }

    public void loadMenu()
    {
        //SceneManager.LoadScene(SceneManager.GetSceneByName("SampleScene").name);
        SceneManager.LoadScene("Menu");
    }

    public void loadAchievments()
    {
        //SceneManager.LoadScene(SceneManager.GetSceneByName("SampleScene").name);
        SceneManager.LoadScene("Achievements");
    }
    public void loadCustomization
        ()
    {
        //SceneManager.LoadScene(SceneManager.GetSceneByName("SampleScene").name);
        SceneManager.LoadScene("Customization");
    }
}
