using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    public Transform gridParent;         
    public GameObject slotPrefab;        

    [Header("Details Panel")]
    public Image detailIcon;
    public TMP_Text detailName;
    public TMP_Text detailDescription;
    public TMP_Text detailAmount;

    private List<GameObject> spawnedSlots = new();

    private void OnEnable()
    {
        RefreshUI();
        ClearDetails();
    }

    public void RefreshUI()
    {
        
        foreach (var obj in spawnedSlots)
            Destroy(obj);
        spawnedSlots.Clear();

        
        foreach (var slot in Inventory.Instance.slots)
        {
            GameObject newSlot = Instantiate(slotPrefab, gridParent);
            spawnedSlots.Add(newSlot);

            
            Image icon = newSlot.transform.Find("Icon").GetComponent<Image>();
            TMP_Text amountText = newSlot.transform.Find("AmountText").GetComponent<TMP_Text>();

            icon.sprite = slot.item.icon;
            amountText.text = slot.amount > 1 ? slot.amount.ToString() : "";

            
            Button btn = newSlot.GetComponent<Button>();
            btn.onClick.AddListener(() => ShowDetails(slot));
        }
    }

    private void ShowDetails(InventorySlot slot)
    {
        detailIcon.sprite = slot.item.icon;
        detailName.text = slot.item.itemName;

        
        detailDescription.text = slot.item.name; 

        detailAmount.text = "Carrying: " + slot.amount;
    }

    private void ClearDetails()
    {
        detailIcon.sprite = null;
        detailName.text = "";
        detailDescription.text = "";
        detailAmount.text = "";
    }
}