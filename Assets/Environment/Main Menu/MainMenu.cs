
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelOneSceneName = "LevelOneScene";
    public GameObject mainMenuUI;
    public GameObject backButtonUI;

    public void Play()
    {
        // Load LevelOne scene
        SceneManager.LoadScene(levelOneSceneName);
    }

    public void Quit()
    {
        // Quit the game
        Application.Quit();
    }

    public void GoBack()
    {
        // Show the main menu and hide the back button
        mainMenuUI.SetActive(true);
        backButtonUI.SetActive(false);
    }

    public void ShowBackButton()
    {
        // Show the back button and hide the main menu
        mainMenuUI.SetActive(false);
        backButtonUI.SetActive(true);
    }
}
