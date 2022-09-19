using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    [Header("Info")]
    public float itemSlotWidthSize = 200f;
    public float itemSlotHeightSize = 250f;

    [Header("Components")]
    private Inventory inventory;
    public GameObject inventoryMenu;
    public Transform itemSlotContainer;
    public Transform itemSlot;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlot = itemSlotContainer.Find("ItemSlot");

        inventoryMenu.SetActive(false);
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlot)
                continue;

            Destroy(child.gameObject);
        }
        //Debug.Log("Called");

        int x = 0;
        int y = 0;
        //float itemSlotCellSize = 100f;
        foreach (Fish fish in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlot, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotWidthSize, -y * itemSlotHeightSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = fish.GetSprite();

            // Sets up the fish text UI to the item slot
            TextMeshProUGUI fishAmountText = itemSlotRectTransform.Find("FishAmount").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI fishValueText = itemSlotRectTransform.Find("FishValue").GetComponent<TextMeshProUGUI>();
            if (fish.amount > 0) // if the fish amount is greater than 1 then show the text
                fishAmountText.SetText(fish.amount.ToString());

            if (fish.value > 0)
                fishValueText.SetText("Value: " + fish.value.ToString());

            x++;
            if (x >= 5)
            {
                x = 0;
                y++;
            }
        }
    }

    public void ClearInventory()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlot)
                continue;

            Destroy(child.gameObject);
        }
        //RefreshInventoryItems();
    }
}
