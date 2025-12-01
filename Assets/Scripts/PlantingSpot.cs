using UnityEngine;

public class PlantingSpot : MonoBehaviour
{
    public bool isPlanted = false;
    public GameObject currentPlant;

    public void Plant(GameObject plantPrefab)
    {
        if (isPlanted) return;

        currentPlant = Instantiate(
            plantPrefab,
            transform.position,
            Quaternion.identity
        );

       
        int plantLayer = LayerMask.NameToLayer("Plant");
        if (plantLayer != -1)   
        {
            SetLayerRecursively(currentPlant, plantLayer);
        }

        isPlanted = true;
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}