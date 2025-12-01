using UnityEngine;

public class PlantingManager : MonoBehaviour
{
    public Transform player;
    public float plantingRange = 1.5f;
    public LayerMask plantingLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            TryPlant();
    }

    void TryPlant()
    {
        Item selectedItem = GetHeldItem();

        if (selectedItem == null)
        {
            Debug.Log("No item available to plant.");
            return;
        }

        if (selectedItem.plantPrefab == null)
        {
            Debug.Log("Item cannot be planted: " + selectedItem.itemName);
            return;
        }

        Ray ray = new Ray(player.position + Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 2f, plantingLayer))
        {
            PlantingSpot spot = hit.collider.GetComponentInParent<PlantingSpot>();
            if (spot == null)
                spot = hit.collider.GetComponent<PlantingSpot>();

            if (spot != null)
            {
                if (!spot.isPlanted)
                {
                    spot.Plant(selectedItem.plantPrefab);

                    Inventory.Instance.Remove(selectedItem);

                    Debug.Log("Planted: " + selectedItem.itemName);
                }
                else
                {
                    Debug.Log("Already planted here.");
                }
            }
            else
            {
                Debug.Log("Hit object but no PlantingSpot attached.");
            }
        }
        else
        {
            Debug.Log("Player not standing over a planting spot.");
        }
    }


    Item GetHeldItem()
    {
        if (Inventory.Instance.slots.Count == 0)
            return null;

        return Inventory.Instance.slots[0].item;
    }
}