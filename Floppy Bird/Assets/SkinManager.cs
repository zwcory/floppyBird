using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance;

    public Sprite redBird, spaceBird;
    public Sprite redWing, spaceWing;

    public Sprite selectedBirdSkin;
    public Sprite selectedWingSkin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setBird(Sprite birdSprite)
    {
        selectedBirdSkin = birdSprite;
        Debug.Log("Set bird sprite  to: " + birdSprite.name);
    }

    public void setWing(Sprite wingSprite)
    {
        selectedWingSkin = wingSprite;
        Debug.Log("Set bird sprite  to: " + wingSprite.name);

    }


    public void ApplySelectedSkins()
    {
        GameObject bird = GameObject.FindGameObjectWithTag("Bird");
        GameObject wing = GameObject.FindGameObjectWithTag("Wing");

        if (bird != null && selectedBirdSkin != null)
        {
            bird.GetComponent<SpriteRenderer>().sprite = selectedBirdSkin;
        }

        if (wing != null && selectedWingSkin != null)
        {
            wing.GetComponent<SpriteRenderer>().sprite = selectedWingSkin;
        }
        
    }
}