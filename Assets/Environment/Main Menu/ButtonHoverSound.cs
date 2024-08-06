//////////////////////////////////////////////////////
// Assignment/Lab/Project: 2D Game
//Name: Ahmed Suoamra
//Section: 2023SU.SGD.113.0073
//Instructor: George Cox
// Date: 10/17/2023
//////////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            if (gameObject.activeInHierarchy) 
            {
                StartCoroutine(PlayClickSoundWithDelay());
            }
        }
    }

    private System.Collections.IEnumerator PlayClickSoundWithDelay()
    {
        audioSource.enabled = true;
        audioSource.PlayOneShot(clickSound);
        yield return new WaitForSeconds(clickSound.length);
        audioSource.enabled = false;
    }
}
