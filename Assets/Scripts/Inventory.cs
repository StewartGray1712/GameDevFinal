using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int capacity = 20;
    public List<InventorySlot> slots = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public bool Add(Item item)
    {
        
        foreach (var slot in slots)
        {
            if (slot.item == item && !slot.IsFull)
            {
                slot.amount++;
                return true;
            }
        }

        
        if (slots.Count < capacity)
        {
            slots.Add(new InventorySlot(item, 1));
            return true;
        }

        Debug.Log("Inventory full!");
        return false;
    }

    public void Remove(Item item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == item)
            {
                slots[i].amount--;

                if (slots[i].amount <= 0)
                    slots.RemoveAt(i);

                return;
            }
        }
    }
}