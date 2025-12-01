using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject worldPrefab;

    [Header("Planting Settings")]
    public bool canPlant = false;
    public GameObject plantPrefab;  

    [TextArea]
    public string description;

    [Header("Stack Settings")]
    public int maxStack = 30;
}
