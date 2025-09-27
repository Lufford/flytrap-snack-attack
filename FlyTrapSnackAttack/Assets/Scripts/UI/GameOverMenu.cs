using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    //Send to Title
    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    //Exit Game
    public void QuitGame()
    {
        Application.Quit();
    }
}
