using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckboxUpdater : MonoBehaviour
{
    private Image box;
    public Color achievedColour = Color.white;
    [SerializeField] Sprite checkedBox;
    [SerializeField] Color uncheckedColour;
    [SerializeField] Sprite uncheckedBox;

    private void Awake()
    {
        box = GetComponent<Image>();
    }

    // used to check box, refactoring this would involve updating a lot of in game components to the new methods name manually
    public void UpdateBox()
    {
        box.color = achievedColour;
        box.sprite = checkedBox;
    }

    // used to uncheck box
    public void UncheckBox()
    {
        box.color = uncheckedColour;
        box.sprite = uncheckedBox;
    }


}
