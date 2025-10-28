using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {

        // todo fix ui updater
        var achievements = AchievementManager.instance.achievements;
        Debug.Log($" Achievements are: {achievements.Count}");
        foreach (var achievement in achievements)
        {
            Debug.Log($" achievement {achievement.title} ,  achieved: {achievement.achieved}");
            if (achievement.achieved)
                achievement.UpdateUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
