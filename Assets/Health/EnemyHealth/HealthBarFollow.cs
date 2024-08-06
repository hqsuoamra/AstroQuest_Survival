using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Vector3 offset;

    void Start()
    {
        // Set the initial local position to the offset
        transform.localPosition = offset;
    }
}
