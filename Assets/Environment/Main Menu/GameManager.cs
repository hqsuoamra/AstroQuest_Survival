using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image startingSceneTransitionImage;
    [SerializeField] private AudioSource transitionSound; // Reference to the AudioSource component

    public Sprite startTransitionSprite; // Sprite for the start transition

    private bool isTransitioning = false; // Flag to track if a transition is in progress

    private void Start()
    {
        // Set the sprite for the starting scene transition
        if (startingSceneTransitionImage != null)
        {
            startingSceneTransitionImage.sprite = startTransitionSprite;
            startingSceneTransitionImage.gameObject.SetActive(true);

            // Start a coroutine to disable the starting scene transition after a delay
            StartCoroutine(DisableStartingSceneTransition());
        }
    }

    private IEnumerator DisableStartingSceneTransition()
    {
        // Wait for 5 seconds before disabling the starting scene transition
        yield return new WaitForSeconds(2.8f);

        // Disable the starting scene transition
        if (startingSceneTransitionImage != null)
        {
            startingSceneTransitionImage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the space key was just pressed and a transition is not already in progress
        if (Input.GetKeyDown(KeyCode.N) && !isTransitioning)
        {
            // Start a coroutine to load the next level after a delay
            StartCoroutine(LoadNextLevel());
        }
    }

    private IEnumerator LoadNextLevel()
    {
        isTransitioning = true; // Set the flag to true to prevent multiple transitions

        // Play the transition sound
        if (transitionSound != null)
        {
            transitionSound.Play();
        }

        // Wait for 1.5 seconds before loading the next level
        yield return new WaitForSeconds(1f);

        // Load the next level based on the current scene
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        switch (currentSceneName)
        {
            case "SampleScene":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
                break;
            case "Level2":
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                break;
            default: // If the current scene is not "SampleScene" or "Level2", load "SampleScene"
                UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
                break;
        }

        isTransitioning = false; // Reset the flag after the transition is complete
    }
}
