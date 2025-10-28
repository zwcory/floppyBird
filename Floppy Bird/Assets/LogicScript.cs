using UnityEngine;
using TMPro;

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

    public AchievementManager achievementManager;

    AudioManager audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void Start()
    {
        coins = PlayerPrefs.GetFloat("Coins", 0f);
        totalCoins = PlayerPrefs.GetFloat("TotalCoins", 0f);
        plays = PlayerPrefs.GetInt("Plays" , 0);
        totalPoints += PlayerPrefs.GetInt("TotalPoints", 0);
        highScore += PlayerPrefs.GetInt("HighScore", 0);

        highScoreText.text = highScore.ToString();
        achievementManager = GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>();
        achievementManager.PatchLogic();
        achievementManager.InitializeAchievements();
    }


    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
        coins ++;
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

        StartCoroutine(levelLoader.LoadSceneByName("MainGame"));
    }

    public void gameOver()
    {
        PlayerPrefs.SetFloat("Coins", coins);
        gameOverScene.SetActive(true);
    }

    public void addCoin(int coin)
    {
        coins += coin;
        totalCoins += coin;
        PlayerPrefs.SetFloat("Coins", coins);
        PlayerPrefs.SetFloat("TotalCoins", totalCoins);
    }

}
 