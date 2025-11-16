using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    // TODO
    // CREATE ActiveMode pref


    [SerializeField] Sprite redBird, spaceBird;
    [SerializeField] Sprite redWing, spaceWing;
    [SerializeField] Sprite whiteWing, coiny;
    [SerializeField] Sprite gingerbreadWing, santa;
    [SerializeField] Sprite conceptWing, concept;




    public SkinPurchaser skinPurchaser;
    public ModePurchaser modePurchaser;
    public TitleLogic titleLogic;
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
        ApplySavedMode();
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

    public void SelectSantaSkin()
    {
        PlayerPrefs.SetString("SelectedSkin", "Santa");
        PlayerPrefs.Save();
        ApplySavedSkin();
        audioManager.PlaySFX(audioManager.selectClip);
        Debug.Log("Selected Santa skin");
        skinPurchaser.SelectBox("Santa");
    }

    public void SelectConceptSkin()
    {
        PlayerPrefs.SetString("SelectedSkin", "Concept");
        PlayerPrefs.Save();
        ApplySavedSkin();
        audioManager.PlaySFX(audioManager.selectClip);
        Debug.Log("Selected Concept skin");
        skinPurchaser.SelectBox("Concept");
    }

    public void SelectDefaultMode()
    {
        
        PlayerPrefs.SetString("SelectedMode", "Default");
        PlayerPrefs.Save();
        ApplySavedMode();
        audioManager.PlaySFX(audioManager.selectClip);
        Debug.Log("Selected Default mode");
        modePurchaser.SelectBox("Default");
    }
    public void SelectChristmasMode()
    {
        wasModeChanged = 1;
        PlayerPrefs.SetInt("WasModeChanged", 1);
        PlayerPrefs.SetString("SelectedMode", "Christmas");
        PlayerPrefs.Save();
        ApplySavedMode();
        audioManager.PlaySFX(audioManager.selectClip);
        Debug.Log("Selected Christmas mode");
        modePurchaser.SelectBox("Christmas");
    }
    public void SelectUnderWaterMode()
    {
        wasModeChanged = 1;
        PlayerPrefs.SetInt("WasModeChanged", 1);
        PlayerPrefs.SetString("SelectedMode", "Underwater");
        PlayerPrefs.Save();
        ApplySavedMode();
        audioManager.PlaySFX(audioManager.selectClip);
        Debug.Log("Selected Underwater mode");
        modePurchaser.SelectBox("Underwater");
    }

    private void ApplySavedMode()
    {
        string selectedMode = PlayerPrefs.GetString("SelectedMode", "Default");
        if (titleLogic != null)
        {
            titleLogic.updateSelectedMode(selectedMode);
        }
        if (modePurchaser != null && modePurchaser.modes != null)
        {
            modePurchaser.SelectBox(selectedMode);
        }
        else
        {
            // Delay the SelectBox call until the next frame
            StartCoroutine(DelayedModeSelect(selectedMode));
        }

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
            else if (selectedSkin == "Santa")
            {
                bird.GetComponent<SpriteRenderer>().sprite = santa;
                wing.GetComponent<SpriteRenderer>().sprite = gingerbreadWing;
            }
            else if (selectedSkin == "Concept")
            {
                bird.GetComponent<SpriteRenderer>().sprite = concept;
                wing.GetComponent<SpriteRenderer>().sprite = conceptWing;
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

    private System.Collections.IEnumerator DelayedModeSelect(string selectedMode)
    {
        yield return null; // Wait one frame

        // Try again after initialization should be complete
        if (modePurchaser != null && modePurchaser.modes != null)
        {
            modePurchaser.SelectBox(selectedMode);
        }
        else
        {
            Debug.LogWarning("ModePurchaser still not initialized after delay");
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
