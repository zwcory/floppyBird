using TMPro;
using UnityEngine;


public class TitleLogic : MonoBehaviour
{
    public LevelLoader levelLoader;
    public Animator menuLeft;
    public Animator achivements;
    public Animator customization;

    AudioManager audioManager;

    private float coins;
    private float coinConverter;
    public TextMeshProUGUI coinsTextMenu;
    public TextMeshProUGUI coinsTextCustomize;
    private string coinsText;


    public void Start()
    {
        // TODO CHANGE TO 0 INSTEAD OF 100, AND IN LOGIC SCRIPT
        coins = PlayerPrefs.GetFloat("Coins", 100f);
        if (coins < 1000)
        {
            coinsText = coins.ToString();

        }
        else if (coins > 1000f && coins < 1000000)
        {
            coinConverter = coins / 1000;
            coinsText = (coinConverter.ToString("F2") + "K");
        }
        else if (coins > 1000000 && coins < 1000000000)
        {
            coinConverter = coins / 1000000;
            coinsText = (coinConverter.ToString("F2") + "M");
        }
        else
        {
            coinConverter = coins / 1000000000;
            coinsText = (coinConverter.ToString("F2") + "B");
        }
        coinsTextMenu.text = coinsText;
        coinsTextCustomize.text = coinsText;
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void startGame()
    {
        audioManager.PlaySFX(audioManager.selectClip);
        StartCoroutine(levelLoader.LoadSceneByName("MainGame"));
    }

    public void loadMenu()
    {
        audioManager.PlaySFX(audioManager.selectClip);

        StartCoroutine(levelLoader.LoadSceneByName("Menu"));

    }

    public void LoadAchievements()
    {
        audioManager.PlaySFX(audioManager.swipeClip);
        achivements.SetTrigger("MR_Entry");
        menuLeft.SetTrigger("ML_Exit");
    }

    public void AchievementsToMenu()
    {
        audioManager.PlaySFX(audioManager.swipeClip);
        menuLeft.SetTrigger("ML_Entry");
        achivements.SetTrigger("MR_Exit");
    }

    public void LoadCustomization()
    {
        audioManager.PlaySFX(audioManager.swipeClip);
        menuLeft.SetTrigger("ML_Exit");
        customization.SetTrigger("MR_Entry");
    }

    public void CustomizationToMenu()
    {
        audioManager.PlaySFX(audioManager.swipeClip);
        menuLeft.SetTrigger("ML_Entry");
        customization.SetTrigger("MR_Exit");
    }
}
