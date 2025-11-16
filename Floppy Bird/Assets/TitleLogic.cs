using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


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
    public string mode;
    public void Start()
    {
        coins = PlayerPrefs.GetFloat("Coins", 0);
        mode = PlayerPrefs.GetString("SelectedMode", "Default");
    }
    private void Update()
    {
        UpdateCoinDisplay();
    }
    private string FormatCoins(float coinAmount, string decimalPlaces)
    {
        if (coinAmount >= 1000000000)
        {
            return (coinAmount / 1000000000).ToString(decimalPlaces) + "B";
        }
        else if (coinAmount >= 1000000)
        {
            return (coinAmount / 1000000).ToString(decimalPlaces) + "M";
        }
        else if (coinAmount >= 1000)
        {
            return (coinAmount / 1000).ToString(decimalPlaces) + "K";
        }
        else
        {
            return coinAmount.ToString("F0"); // Use F0 for whole numbers
        }
    }

    private void UpdateCoinDisplay()
    {
        coins = PlayerPrefs.GetFloat("Coins", 0); 
        string formattedCoinsF2 = FormatCoins(coins, "F2");
        string formattedCoinsF0 = FormatCoins(coins, "F1");
        if (coinsTextMenu != null) coinsTextMenu.text = formattedCoinsF2;
        if (coinsTextCustomize != null) coinsTextCustomize.text = formattedCoinsF0;
    }

    public void updateSelectedMode(string selectedMode)
    {
        mode = selectedMode;

    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void startGame()
    {
        audioManager.PlaySFX(audioManager.selectClip);
        StartCoroutine(levelLoader.LoadSceneByName(mode));
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
