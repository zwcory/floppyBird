using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class ColourChanger : MonoBehaviour
{
    private TextMeshProUGUI text;
    public Color achievedColour = Color.white;
    [SerializeField] Color unselectedColour;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Used to change to white, refactoring this would involve updating a lot of in game components to the new methods name manually

    public void ChangeColour()
    {
        text.color = achievedColour;
    }

    public void SetUnselectedColour()
    {
        text.color = unselectedColour;
    }
}
