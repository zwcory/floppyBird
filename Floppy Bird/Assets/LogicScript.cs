using UnityEngine;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScene;
    public TextMeshProUGUI highScore;
    public LevelLoader levelLoader;

    private float coins;


    AudioManager audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void Start()
    {
        // TODO CHANGE TO 0 INSTEAD OF 100, AND IN TITLE LOGIC
        coins = PlayerPrefs.GetFloat("Coins", 100f);
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }


    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
        coins ++;
        if (playerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            highScore.text = playerScore.ToString();
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
        PlayerPrefs.SetFloat("Coins", coins);
    }

}
 