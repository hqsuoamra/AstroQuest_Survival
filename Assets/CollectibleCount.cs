using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this

public class CollectibleCount : MonoBehaviour
{
    TMPro.TMP_Text text;
    int count;

    void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    void Start() => UpdateCount();

    void OnEnable() => Collectible.OnCollected += OnCollectibleCollected;
    void OnDisable() => Collectible.OnCollected -= OnCollectibleCollected;

    void OnCollectibleCollected()
    {
        count++;
        UpdateCount();

        if (count >= 1) // Check if 6 collectibles are collected /// must be 6
        {
            SceneManager.LoadScene("EndScene"); // Load the EndScene
        }
    }

    void UpdateCount()
    {
        text.text = $"Collected items: {count} / 6";
    }
}
