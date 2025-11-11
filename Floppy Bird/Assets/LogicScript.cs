using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScene;
    public TextMeshProUGUI highScoreText;
    public LevelLoader levelLoader;

    private float coins;
    public float totalCoins;
    public int plays;
    public int totalPoints;
    public int highScore;

    public int speedIncreaser = 15;
    public int speedIncreaserCount;
    public AchievementManager achievementManager;

    [Header("Difficulty References")]
    public PipeSpawnerScript pipeSpawn;     
    public PipeMoveScript pipeMoveScript;   
    public CoinMoveScript coinMoveScript;





    AudioManager audioManager;

    private bool[] difficultyTriggered = new bool[20];




    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void Start()
    {

        coins = PlayerPrefs.GetFloat("Coins", 0f);
        totalCoins = PlayerPrefs.GetFloat("TotalCoins", 0f);
        Debug.Log($"total coins in logic {totalCoins}");
        plays = PlayerPrefs.GetInt("Plays" , 0);
        totalPoints += PlayerPrefs.GetInt("TotalPoints", 0);
        highScore += PlayerPrefs.GetInt("HighScore", 0);

        highScoreText.text = highScore.ToString();
        achievementManager = GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>();
        achievementManager.PatchLogic();
        achievementManager.PatchSkinManager();
        achievementManager.InitializeAchievements();
    }

    private void Update()
    {
        // TESTING ONLY - Remove this later!
        if (Input.GetKeyDown(KeyCode.T))
        {
            addScore(5); // Quickly add 5 points to test
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
           Debug.Log($"Pipe speed {pipeMoveScript.getMoveSpeed()}");
        }
    }

    private void CheckDifficultyIncrease()
    {
        // Gradual increase every 5 points
        if (playerScore > 0 && playerScore % 5 == 0)
        {
            int arrayIndex = playerScore / 5;
            if (arrayIndex >= difficultyTriggered.Length)
            {
                Debug.Log($"Score {playerScore} exceeds difficulty array bounds - no further increases.");
                return;
            }

            if (!difficultyTriggered[arrayIndex])
            {

                pipeSpawn.setSpawnRate(Mathf.Max(1.5f, pipeSpawn.getSpawnRate() - 0.075f));
                pipeMoveScript.setMoveSpeed(Mathf.Min(10f, pipeMoveScript.getMoveSpeed() + 0.5f));
                coinMoveScript.setMoveSpeed(Mathf.Min(10f, coinMoveScript.getMoveSpeed() + 0.5f));


                difficultyTriggered[playerScore / 5] = true;
                Debug.Log($"Difficulty increased at score {playerScore}!");
            }
        }
    }

   

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        totalPoints += scoreToAdd;
        speedIncreaserCount++;
        scoreText.text = playerScore.ToString();
        coins ++;
        totalCoins ++;
        CheckDifficultyIncrease();
        if (playerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            highScoreText.text = playerScore.ToString();
            PlayerPrefs.SetInt("HighScore", playerScore);
            Debug.Log("New High Score " + playerScore);
        }
    }


    public void restartGame()
    {
        audioManager.PlaySFX(audioManager.selectClip);
        pipeSpawn.setSpawnRate(3f);
        StartCoroutine(levelLoader.LoadSceneByName("MainGame"));
    }

    public void gameOver()
    {
        plays++;
        pipeMoveScript.setMoveSpeed(5f);
        coinMoveScript.setMoveSpeed(5f);
        PlayerPrefs.SetFloat("Coins", coins);
        PlayerPrefs.SetFloat("TotalCoins", totalCoins);
        PlayerPrefs.SetInt("TotalPoints", totalPoints);
        PlayerPrefs.SetInt("Plays", plays);
        gameOverScene.SetActive(true);
        PlayerPrefs.Save();

    }

    public void addCoin(int coin)
    {
        coins += coin;
        totalCoins += coin;
        
    }

}
 