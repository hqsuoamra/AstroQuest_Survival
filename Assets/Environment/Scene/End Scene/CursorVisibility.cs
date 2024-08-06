using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        UpdateCursorVisibility();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateCursorVisibility();
    }

    void UpdateCursorVisibility()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "EndScene" || currentScene == "MainMenu_dst")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (currentScene == "Playground")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
