using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public GameObject pickupPrompt;
    public AudioClip pickupSFX;
    private AudioSource audioSource;

    private bool playerInRange = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.spatialBlend = 1f;
        audioSource.playOnAwake = false;

        if (pickupPrompt != null)
            pickupPrompt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (pickupPrompt != null)
                pickupPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pickupPrompt != null)
                pickupPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        bool pickupKey =
            Input.GetMouseButtonDown(0) ||
            InputSimulator.CheckMouseClick();

        if (playerInRange && pickupKey)
        {
            if (Inventory.Instance.Add(item))
            {
                if (pickupSFX != null)
                    audioSource.PlayOneShot(pickupSFX, 2.0f);

                if (pickupPrompt != null)
                    pickupPrompt.SetActive(false);

                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.isKinematic = true;
                }

                float delay = pickupSFX != null ? pickupSFX.length : 0f;
                Destroy(gameObject, delay);
            }
        }
    }
}