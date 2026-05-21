using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;

        if (AudioManager.instance != null)
            AudioManager.instance.sceneindex = 0;

        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}