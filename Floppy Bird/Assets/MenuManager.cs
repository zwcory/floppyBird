using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public ColourChanger[] colourChanger;
    public GameObject textHolder;
    public CheckboxUpdater checkboxUpdater;
    private void Start()
    {
        // find achievements that have been instantiated
        var achievements = AchievementManager.instance.achievements;
        foreach (var achievement in achievements)
        {

            textHolder = GameObject.FindGameObjectWithTag(achievement.title);
            

            if (achievement.achieved)
            {
                if (textHolder != null)
                {
                    colourChanger = textHolder.GetComponentsInChildren<ColourChanger>();
                    checkboxUpdater = textHolder.GetComponentInChildren<CheckboxUpdater>();

                    foreach (var text in colourChanger)
                    {
                        Debug.Log($" colourChanger: {text}");
                        text.ChangeColour();
                    }

                    checkboxUpdater.UpdateBox();
                }
            }
            
        }
    }

    void Update()
    {
        
    }
}
