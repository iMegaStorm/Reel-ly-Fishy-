using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeScript : MonoBehaviour
{
    [Header("Upgrade Prices")]
    public int inventoryUpgradePrice;
    public int hookRangeUpgradePrice;
    public int catchRateUpgradePrice;
    public int speedUpgradePrice;

    [Header("Upgrade Text")]
    public TextMeshProUGUI inventoryUpgradeText;
    public TextMeshProUGUI hookRangeUpgradeText;
    public TextMeshProUGUI catchRateUpgradeText;
    public TextMeshProUGUI speedUpgradeText;
    public TextMeshProUGUI shopOwnerText;

    [Header("Upgrade Objects")]
    public GameObject inventoryObj;
    public GameObject hookRangeObj;
    public GameObject catchRateObj;
    public GameObject speedObj;

    public AudioSource audio;

    public bool catchRateUpgrade;
    public static UpgradeScript instance;

    private void Start()
    {
        instance = this;

        inventoryUpgradeText.SetText(inventoryUpgradePrice.ToString());
        hookRangeUpgradeText.SetText(hookRangeUpgradePrice.ToString());
        catchRateUpgradeText.SetText(catchRateUpgradePrice.ToString());
        speedUpgradeText.SetText(speedUpgradePrice.ToString());
        //shopOwnerText.SetText("guyg!");
    }

    public void UpgradeInventory()
    {
        if (Player.instance.gems >= inventoryUpgradePrice)
        {
            Player.instance.maxFish = 10;
            Player.instance.gems -= inventoryUpgradePrice;
            inventoryObj.SetActive(false);
            shopOwnerText.text = ("You have purchased: Inventory Upgrade");
            audio.Play();
        }
        else
            FailedToUpgrade();
    }

    public void UpgradeHookRange()
    {
        if (Player.instance.gems >= hookRangeUpgradePrice)
        {
            Player.instance.baitCollider.GetComponent<CircleCollider2D>().radius = 5f;
            Player.instance.gems -= hookRangeUpgradePrice;
            hookRangeObj.SetActive(false);
            shopOwnerText.text = ("You have purchased: Hook Range Upgrade");
            audio.Play();
        }
        else
            FailedToUpgrade();
    }

    public void UpgradeCatchRate()
    {
        if (Player.instance.gems >= catchRateUpgradePrice)
        {
            Player.instance.gems -= catchRateUpgradePrice;
            catchRateObj.SetActive(false);
            catchRateUpgrade = true;
            shopOwnerText.text = ("You have purchased: Catch Rate Upgrade");
            if (!audio.isPlaying)
                audio.Play();
        }
        else
            FailedToUpgrade();
    }

    public void UpgradeSpeed()
    {
        if (Player.instance.gems >= speedUpgradePrice)
        {
            Player.instance.accelerationPower = 15f;
            Player.instance.gems -= speedUpgradePrice;
            speedObj.SetActive(false);
            shopOwnerText.text = ("You have purchased: Speed Upgrade");
            audio.Play();
        }
        else
            FailedToUpgrade();
    }

    void FailedToUpgrade()
    {
        shopOwnerText.SetText("You cant afford this upgrade!");
    }
}
