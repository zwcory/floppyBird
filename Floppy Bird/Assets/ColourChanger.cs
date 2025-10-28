using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ColourChanger : MonoBehaviour
{
    private TextMeshProUGUI text;
    public Color achievedColour = Color.white;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeColour()
    {
        text.color = achievedColour;
    }
}
