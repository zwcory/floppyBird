using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public LogicScript logic;
    public SkinManager skinManager;
    [SerializeField] GameObject achievment1;
    [SerializeField] GameObject achievment2;
    [SerializeField] GameObject achievment3;
    [SerializeField] GameObject achievment4;
    [SerializeField] GameObject achievment5;

    public List<Achievement> achievements;
    
    public static AchievementManager instance;


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

    public void PatchSkinManager()
    {
        skinManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<SkinManager>();
    }


    public void InitializeAchievements()
    {
        //if ((logic == null) || (skinManager == null))
        if (logic == null)
        {
            return;
        }
        if (achievements != null)
        {
            return;
        }
        achievements = new List<Achievement>();
        achievements.Add(new Achievement("FirstPoint", "Score your first point", "none", 25, (object o) => logic.playerScore >= 1));
        achievements.Add(new Achievement("50Up", "Score a total of 50 points", "none", 50, (object o) => logic.totalPoints >= 50));
        achievements.Add(new Achievement("CrashCourse", "Play 20 games", "none", 150, (object o) => logic.plays >= 20));
        achievements.Add(new Achievement("HappyFlappy", "Get a high score of 20", "none", 200, (object o) => logic.highScore >= 20));
        achievements.Add(new Achievement("HighFlyer", "Get a high score of 50", "none", 500, (object o) => logic.highScore >= 50));
        achievements.Add(new Achievement("Unstoppable", "Get a high score of 200", "none", 10000, (object o) => logic.highScore >= 200));
        achievements.Add(new Achievement("FeelsFresh", "Change the mode", "none", 500, (object o) => skinManager.wasModeChanged >= 1));
        achievements.Add(new Achievement("FilthyRich", "Earn a total of 20k coins", "Coiny", 0, (object o) => logic.totalCoins >= 20000));

        foreach (var achievement in achievements)
        {
            int wasAchieved = PlayerPrefs.GetInt(achievement.title, 0);
            if (wasAchieved > 0)
            {
                achievement.SetCompletion();
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
            if (itemReward == "Coiny")
            {
                PlayerPrefs.SetInt("Coiny", 1);
            }
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


    public bool RequirementsMet()
    {
        return requirement.Invoke(null);
    }

}
