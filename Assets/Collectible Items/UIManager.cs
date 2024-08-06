using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text itemCountText;
    private int itemCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementItemCount()
    {
        itemCount++;
        itemCountText.text = "Items: " + itemCount;
    }
}
