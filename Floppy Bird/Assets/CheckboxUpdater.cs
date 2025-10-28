using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckboxUpdater : MonoBehaviour
{
    private Image box;
    public Color achievedColour = Color.white;
    [SerializeField] Sprite checkedBox;

    private void Awake()
    {
        box = GetComponent<Image>();
    }

    public void UpdateBox()
    {
        box.color = achievedColour;
        box.sprite = checkedBox;
    }
}
