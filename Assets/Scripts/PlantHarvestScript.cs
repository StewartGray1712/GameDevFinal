using UnityEngine;

public class PlantHarvest : MonoBehaviour
{
    public GameObject tomatoPrefab;
    public int minTomatoes = 1;
    public int maxTomatoes = 3;
    public Transform spawnPoint;
    public GameObject harvestPrompt;

    private bool playerInRange = false;

    private void Start()
    {
        if (harvestPrompt != null)
            harvestPrompt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (harvestPrompt != null)
                harvestPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (harvestPrompt != null)
                harvestPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        bool harvestKey =
            Input.GetKeyDown(KeyCode.E) ||
            InputSimulator.CheckKeyPress(KeyCode.E);

        if (playerInRange && harvestKey)
        {
            Harvest();
        }
    }

    private void Harvest()
    {
        int amount = Random.Range(minTomatoes, maxTomatoes + 1);

        for (int i = 0; i < amount; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-0.3f, 0.3f),
                0.5f,
                Random.Range(-0.3f, 0.3f)
            );

            Vector3 spawnPos = (spawnPoint != null ? spawnPoint.position : transform.position) + offset;
            Instantiate(tomatoPrefab, spawnPos, Quaternion.identity);
        }

        if (harvestPrompt != null)
            harvestPrompt.SetActive(false);

        Destroy(gameObject);
    }
}



