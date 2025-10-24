using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleLogic : MonoBehaviour
{
    public LevelLoader levelLoader;

    public void startGame()
    {
        StartCoroutine(levelLoader.LoadSceneByName("MainGame"));
    }

    public void loadMenu()
    {
        StartCoroutine(levelLoader.LoadSceneByName("Menu"));

    }

    public void loadAchievments()
    {
        StartCoroutine(levelLoader.LoadSceneByName("Achievements"));
    }
    public void loadCustomization
        ()
    {
        StartCoroutine(levelLoader.LoadSceneByName("Customization"));
    }
}
