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
        Debug.Log($" Achievements are: {achievements.Count}");
        foreach (var achievement in achievements)
        {
            Debug.Log($" achievement {achievement.title} ,  achieved: {achievement.achieved}");

            textHolder = GameObject.FindGameObjectWithTag(achievement.title);
            Debug.Log($" textHolder: {textHolder}");

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
