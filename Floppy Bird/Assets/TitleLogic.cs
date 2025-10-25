using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleLogic : MonoBehaviour
{
    public LevelLoader levelLoader;
    public Animator menuLeft;
    public Animator achivements;
    public Animator customization;

    public void startGame()
    {
        StartCoroutine(levelLoader.LoadSceneByName("MainGame"));
    }

    public void loadMenu()
    {
        StartCoroutine(levelLoader.LoadSceneByName("Menu"));

    }

    public void LoadAchievements()
    {
        achivements.SetTrigger("MR_Entry");
        menuLeft.SetTrigger("ML_Exit");
    }

    public void AchievementsToMenu()
    {
        menuLeft.SetTrigger("ML_Entry");
        achivements.SetTrigger("MR_Exit");
    }

    public void LoadCustomization()
    {
        menuLeft.SetTrigger("ML_Exit");
        customization.SetTrigger("MR_Entry");
    }

    public void CustomizationToMenu()
    {
        menuLeft.SetTrigger("ML_Entry");
        customization.SetTrigger("MR_Exit");
    }
}
