using UnityEngine;

public class SkinManager : MonoBehaviour
{
    // TODO
    // CREATE ActiveMode pref


    [SerializeField] Sprite redBird, spaceBird;
    [SerializeField] Sprite redWing, spaceWing;


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
        PlayerPrefs.SetString("SelectedSkin", "Red");
        PlayerPrefs.Save();
        ApplySavedSkin();
        audioManager.PlaySFX(audioManager.selectClip);
        Debug.Log("Selected Red skin");
    }

    public void SelectSpaceSkin()
    {
        PlayerPrefs.SetString("SelectedSkin", "Space");
        PlayerPrefs.Save();
        ApplySavedSkin();
        audioManager.PlaySFX(audioManager.selectClip);
        Debug.Log("Selected Space skin");
    }

    private void ApplySavedSkin()
    {
        string selectedSkin = PlayerPrefs.GetString("SelectedSkin", "Red");


        GameObject bird = GameObject.FindGameObjectWithTag("Bird");
        GameObject wing = GameObject.FindGameObjectWithTag("Wing");

        if (bird != null && wing != null)
        {
            if (selectedSkin == "Red")
            {
                bird.GetComponent<SpriteRenderer>().sprite = redBird;
                wing.GetComponent<SpriteRenderer>().sprite = redWing;
            }
            else if (selectedSkin == "Space")
            {
                bird.GetComponent<SpriteRenderer>().sprite = spaceBird;
                wing.GetComponent<SpriteRenderer>().sprite = spaceWing;
            }
        }
    }
}