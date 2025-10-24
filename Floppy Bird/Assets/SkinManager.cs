using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField] Sprite redBird, spaceBird;
    [SerializeField] Sprite redWing, spaceWing;

    private void Start()
    {
        ApplySavedSkin();
    }

    public void SelectRedSkin()
    {
        PlayerPrefs.SetString("SelectedSkin", "Red");
        PlayerPrefs.Save();
        ApplySavedSkin();
        Debug.Log("Selected Red skin");
    }

    public void SelectSpaceSkin()
    {
        PlayerPrefs.SetString("SelectedSkin", "Space");
        PlayerPrefs.Save();
        ApplySavedSkin();
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