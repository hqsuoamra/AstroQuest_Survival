using UnityEngine;

[RequireComponent(typeof(SomeRequiredComponent))]
public class MyComponent : MonoBehaviour
{
    [SerializeField]
    private SomeRequiredComponent myProperty;

    private void Awake()
    {
        // Ensure that the property is assigned
        if (myProperty == null)
        {
            myProperty = GetComponent<SomeRequiredComponent>();
            if (myProperty == null)
            {
                Debug.LogError("SomeRequiredComponent is missing from the GameObject.");
            }
        }
    }
}
