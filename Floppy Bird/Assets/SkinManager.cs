using System.Drawing;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    // TODO
    // CREATE ActiveMode pref


    [SerializeField] Sprite redBird, spaceBird;
    [SerializeField] Sprite redWing, spaceWing;
    [SerializeField] Sprite whiteWing, coiny;

    public SkinPurchaser skinPurchaser;
    public int wasModeChanged;
    public AchievementManager achievementManager;


    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        wasModeChanged = PlayerPrefs.GetInt("WasModeChanged", 0);
        ApplySavedSkin();
        achievementManager = GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>();
        achievementManager.PatchSkinManager();
    }

    public void SelectRedSkin()
    {
        PlayerPrefs.SetString("SelectedSkin", "RedBird");
        PlayerPrefs.Save();
        ApplySavedSkin();
        audioManager.PlaySFX(audioManager.selectClip);
        Debug.Log("Selected Red skin");
        skinPurchaser.SelectBox("RedBird");

    }

    public void SelectSpaceSkin()
    {
        PlayerPrefs.SetString("SelectedSkin", "SpaceBird");
        PlayerPrefs.Save();
        ApplySavedSkin();
        audioManager.PlaySFX(audioManager.selectClip);
        Debug.Log("Selected Space skin");
        skinPurchaser.SelectBox("SpaceBird");

    }

    public void SelectCoinSkin()
    {
        PlayerPrefs.SetString("SelectedSkin", "Coiny");
        PlayerPrefs.Save();
        ApplySavedSkin();
        audioManager.PlaySFX(audioManager.selectClip);
        Debug.Log("Selected Coin skin");
        skinPurchaser.SelectBox("Coiny");
    }

    private void ApplySavedSkin()
    {
        string selectedSkin = PlayerPrefs.GetString("SelectedSkin", "RedBird");

        GameObject bird = GameObject.FindGameObjectWithTag("Bird");
        GameObject wing = GameObject.FindGameObjectWithTag("Wing");

        if (bird != null && wing != null)
        {
            if (selectedSkin == "RedBird")
            {
                bird.GetComponent<SpriteRenderer>().sprite = redBird;
                wing.GetComponent<SpriteRenderer>().sprite = redWing;
            }
            else if (selectedSkin == "SpaceBird")
            {
                bird.GetComponent<SpriteRenderer>().sprite = spaceBird;
                wing.GetComponent<SpriteRenderer>().sprite = spaceWing;
            }
            else if (selectedSkin == "Coiny")
            {
                bird.GetComponent<SpriteRenderer>().sprite = coiny;
                wing.GetComponent<SpriteRenderer>().sprite = whiteWing;
            }
        }

        Debug.Log($"{selectedSkin} was selected");

        // Check if skinPurchaser is initialized before calling SelectBox
        if (skinPurchaser != null && skinPurchaser.skins != null)
        {
            skinPurchaser.SelectBox(selectedSkin);
        }
        else
        {
            // Delay the SelectBox call until the next frame
            StartCoroutine(DelayedSelectBox(selectedSkin));
        }
    }

    private System.Collections.IEnumerator DelayedSelectBox(string selectedSkin)
    {
        yield return null; // Wait one frame

        // Try again after initialization should be complete
        if (skinPurchaser != null && skinPurchaser.skins != null)
        {
            skinPurchaser.SelectBox(selectedSkin);
        }
        else
        {
            Debug.LogWarning("SkinPurchaser still not initialized after delay");
        }
    }
}