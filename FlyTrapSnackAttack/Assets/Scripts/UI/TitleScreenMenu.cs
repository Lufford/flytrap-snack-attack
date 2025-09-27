using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenMenu : MonoBehaviour
{
    //Send to Game
    public void StartGame()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    //Exit Game
    public void QuitGame()
    {
        Application.Quit();
    }
}