using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenMenu : MonoBehaviour
{
    //Send to Game
    public void StartGame()
    {
        GameManager.Instance.StartGame();
        SceneManager.LoadScene("FirstLevel");
    }

    //Exit Game
    public void QuitGame()
    {
        #if !UNITY_EDITOR
            Application.Quit();
        #elif UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}