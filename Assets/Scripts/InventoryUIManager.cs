using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUIManager : MonoBehaviour
{
    [Header("UI References")]
    public Canvas gameplayUI;
    public Canvas inventoryUI;
    public Button inventoryButton;

    [Header("Inventory Card Layout")]
    public Transform cardParent;
    public GameObject cardPrefab;

    private bool inventoryOpen = false;
    private List<GameObject> spawnedCards = new();

    void Start()
    {
        inventoryUI.enabled = false;

        
        cardPrefab.SetActive(false);

        if (inventoryButton != null)
            inventoryButton.onClick.AddListener(ToggleInventory);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            ToggleInventory();
    }

    public void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;

        gameplayUI.enabled = !inventoryOpen;
        inventoryUI.enabled = inventoryOpen;

        if (inventoryOpen)
            RefreshCards();
        else
            ClearCards();
    }

    private void RefreshCards()
    {
        ClearCards();

        foreach (var slot in Inventory.Instance.slots)
        {
            GameObject card = Instantiate(cardPrefab, cardParent);

            
            card.SetActive(true);

            spawnedCards.Add(card);

            
            Image icon = card.transform.Find("Icon").GetComponent<Image>();
            TMP_Text nameText = card.transform.Find("ItemName").GetComponent<TMP_Text>();
            TMP_Text amountText = card.transform.Find("Amount").GetComponent<TMP_Text>();
            TMP_Text descriptionText = card.transform.Find("Description").GetComponent<TMP_Text>();

            
            icon.sprite = slot.item.icon;
            nameText.text = slot.item.itemName;
            amountText.text = "x" + slot.amount;
            descriptionText.text = slot.item.description;
        }
    }

    private void ClearCards()
    {
        foreach (GameObject obj in spawnedCards)
            Destroy(obj);

        spawnedCards.Clear();
    }
}
