using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float walkSpeed, walkBackSpeed, originalWalkSpeed, runSpeed, rotationSpeed;
    public bool isWalking;
    public Transform playerTransform;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerRigid.velocity = transform.forward * walkSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerRigid.velocity = -transform.forward * walkBackSpeed * Time.deltaTime;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleRunning();
    }

    void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            isWalking = true;
            Debug.Log("Trigger walk set");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            isWalking = false;
            Debug.Log("Trigger idle set");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("walkBack");
            playerAnim.ResetTrigger("idle");
            Debug.Log("Trigger walkBack set");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("walkBack");
            playerAnim.SetTrigger("idle");
            Debug.Log("Trigger idle set");
        }
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerTransform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTransform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    void HandleRunning()
    {
        if (isWalking)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                walkSpeed += runSpeed;
                playerAnim.SetTrigger("run");
                playerAnim.ResetTrigger("walk");
                Debug.Log("Trigger run set");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                walkSpeed = originalWalkSpeed;
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("walk");
                Debug.Log("Trigger walk set");
            }
        }
    }
}
