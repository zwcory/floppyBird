using UnityEngine;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScene;
    public TextMeshProUGUI highScore;
    public LevelLoader levelLoader;

    public void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }


    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();

        if (playerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            highScore.text = playerScore.ToString();
            PlayerPrefs.SetInt("HighScore", playerScore);
            Debug.Log("New High Score " + playerScore);
        }
    }


    public void restartGame()
    {
        StartCoroutine(levelLoader.LoadSceneByName("MainGame"));
    }

    public void gameOver()
    {
        gameOverScene.SetActive(true);
    }
}
 