using UnityEngine;

public class HealthBarPosition : MonoBehaviour
{
    public Transform target; // The AnkleGrabber's transform
    public Vector3 offset;   // Offset for the health bar position

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.rotation = Camera.main.transform.rotation; // Ensure it faces the camera
        }
    }
}
