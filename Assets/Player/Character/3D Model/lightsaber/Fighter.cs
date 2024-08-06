using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using THCHealthSystem; // Add this line at the top of the script
using AnkleGrabberHealthScripts; // Ensure this namespace is used if the AnkleGrabberHealth class is in this namespace

public class Fighter : MonoBehaviour
{
    private Animator anim;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            anim.SetBool("hit3", false);
            noOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            anim.SetBool("hit1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false);
            anim.SetBool("hit2", true);
        }
        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
            anim.SetBool("hit3", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider entered with: " + other.gameObject.name);
        
        if (other.CompareTag("THC6"))
        {
            THCHealth thcHealth = other.GetComponent<THCHealth>();
            if (thcHealth != null)
            {
                Debug.Log("Damaging THC6.");
                thcHealth.TakeDamage(1);
            }
            else
            {
                Debug.LogWarning("THCHealth component not found on the object with tag THC6.");
            }
        }
        else if (other.CompareTag("AnkleGrabber"))
        {
            AnkleGrabberHealth ankleGrabberHealth = other.GetComponent<AnkleGrabberHealth>();
            if (ankleGrabberHealth != null)
            {
                Debug.Log("Damaging AnkleGrabber.");
                ankleGrabberHealth.TakeDamage(1);
            }
            else
            {
                Debug.LogWarning("AnkleGrabberHealth component not found on the object with tag AnkleGrabber.");
            }
        }
    }
}
