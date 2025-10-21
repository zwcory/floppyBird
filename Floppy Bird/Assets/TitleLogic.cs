using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleLogic : MonoBehaviour
{
    
    public void startGame()
    {
        //SceneManager.LoadScene(SceneManager.GetSceneByName("SampleScene").name);
        SceneManager.LoadScene("SampleScene");
    }

}
