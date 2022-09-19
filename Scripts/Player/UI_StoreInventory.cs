using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_StoreInventory : MonoBehaviour
{
    [Header("Info")]
    public float itemSlotWidthSize = 200f;
    public float itemSlotHeightSize = 250f;

    [Header("Components")]
    private Inventory inventory;
    public GameObject shopMenu;
    public Transform shopSlotContainer;
    public Transform shopSlot;

    // Start is called before the first frame update
    void Start()
    {

        shopSlotContainer = transform.Find("ShopSlotContainer");
        shopSlot = shopSlotContainer.Find("ShopSlot");
        shopMenu.SetActive(false);
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshShopItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshShopItems();
    }

    private void RefreshShopItems()
    {
        foreach (Transform child in shopSlotContainer)
        {
            if (child == shopSlot)
                continue;

            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        //float itemSlotCellSize = 100f;
        foreach (Fish fish in inventory.GetItemList())
        {
            RectTransform shopSlotRectTransform = Instantiate(shopSlot, shopSlotContainer).GetComponent<RectTransform>();
            shopSlotRectTransform.gameObject.SetActive(true);

            shopSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotWidthSize, -y * itemSlotHeightSize);
            Image image = shopSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = fish.GetSprite();

            // Sets up the fish text UI to the item slot
            TextMeshProUGUI fishAmountText = shopSlotRectTransform.Find("ShopFishAmount").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI fishValueText = shopSlotRectTransform.Find("ShopFishValue").GetComponent<TextMeshProUGUI>();
            if (fish.amount > 0) // if the fish amount is greater than 1 then show the text
                fishAmountText.SetText(fish.amount.ToString());

            if (fish.value > 0)
                fishValueText.SetText("Value: " + fish.value.ToString());

            x++;
            if (x >= 4)
            {
                x = 0;
                y++;
            }
        }
    }

    public void SellFish()
    {
        foreach (Transform child in shopSlotContainer)
        {
            if (child == shopSlot)
                continue;

            Destroy(child.gameObject);
        }

        foreach (Fish fish in inventory.GetItemList())
        {
            Player.instance.gems += fish.value;

        }
        Player.instance.curFish = 0;
        inventory.fishList.Clear();
        //RefreshShopItems();
    }
}
