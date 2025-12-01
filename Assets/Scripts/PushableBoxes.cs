using UnityEngine;

public class PushableObject : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        Rigidbody otherRb = collision.rigidbody;

        if (otherRb != null)
        {
            Debug.Log($"{gameObject.name} collided with {otherRb.name} (both Rigidbodies).");
        }
    }
}

