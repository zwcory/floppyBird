using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public LogicScript logic;
    [SerializeField] GameObject achievment1;
    [SerializeField] GameObject achievment2;
    [SerializeField] GameObject achievment3;
    [SerializeField] GameObject achievment4;
    [SerializeField] GameObject achievment5;

    public List<Achievement> achievements;
    

    public int testInt;
    public float testFloat;

    public static AchievementManager instance;


    // TODO
    //  - Move manager object to title screen
    //  - Connect achievement objects to script with tags



    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    // NOT USED RN, CAN BE USED FOR ACHIEVEMENTS THAT ARE UNLOCKED BY COMPLETING ANOTHER ACHIEVEMENT

    public bool AchievementUnlocked(string achievementName)
    {
        bool result = false;

        if (achievements == null)
        {
            return false;

        }

        Achievement[] achivementsArray = achievements.ToArray();
        Achievement a = Array.Find(achivementsArray, ach => achievementName == ach.title);

        if (a == null)
        {
            return false;
        }

        result = a.achieved;

        return result;
    }


    private void Start()
    {
        InitializeAchievements();
    }

    public void PatchLogic()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

    }

    public void InitializeAchievements()
    {
        if (logic == null)
        {
            return;
        }
        if (achievements != null)
        {
            return;
        }
        achievements = new List<Achievement>();
        achievements.Add(new Achievement("FirstPoint", "Score your first point", "none", 109, (object o) => logic.playerScore >= 1));
        // todo -  add a total score pref that updates in logic
        achievements.Add(new Achievement("50Total", "Score a total of 50 points", "none", 504, (object o) => logic.playerScore >= 5));

        foreach (var achievement in achievements)
        {
            int wasAchieved = PlayerPrefs.GetInt(achievement.title, 0);
            if (wasAchieved > 0)
            {
                achievement.SetCompletion();
                achievement.UpdateUI();
            }
        }
    }

    private void Update()
    {
        CheckAchivements();
    }

    private void CheckAchivements() 
    {
        if (achievements == null)
        {
            return;
        }
        foreach (var achievement in achievements)
        {
            achievement.UpdateCompletion();
        }

    }

}


public class Achievement
{
    public Achievement(string title, string description, string itemReward, int coinReward, Predicate<object> requirement)
    {
        this.title = title;
        this.description = description;
        this.itemReward = itemReward;
        this.coinReward = coinReward;
        this.requirement = requirement;
    }

    public string title;
    public string description;
    public string itemReward;
    public int coinReward;
    public Predicate<object> requirement;
    public bool achieved;
    private LogicScript logic;
    private GameObject ui;
    
    public void UpdateCompletion()
    {
        if (achieved)
        {
            return;
        }
        if (RequirementsMet())

        {
            logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
            Debug.Log("Achievement unlocked");
            Debug.Log($"{title}: {description} items rewarded {itemReward} and {coinReward} coins");
            achieved = true;
            logic.addCoin( coinReward );
            PlayerPrefs.SetInt(title, 1);
        }
    }

    // used for setting completion when loading menu, using prefs, grants no rewards
    public void SetCompletion()
    {
        if (achieved)
        {
            return;
        }
            achieved = true;
    }


    // Doesnt work needs fixing
    public void UpdateUI()
    {
        if (ui == null)
        {
            ui = GameObject.FindGameObjectWithTag(title);
            if (ui == null)
            {
                Debug.LogWarning($"No UI found for {title}");
                return;
            }
        }

        var texts = ui.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
            text.color = Color.white;
    }

    public bool RequirementsMet()
    {
        return requirement.Invoke(null);
    }

}
