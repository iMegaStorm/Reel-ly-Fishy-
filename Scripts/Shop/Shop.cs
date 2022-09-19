using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject shop;
    public TextMeshProUGUI gemsText;

    private void Start()
    {
        UpdateGems();
    }

    public void OnClose()
    {
        shop.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Player.instance.isPaused = false;
        Player.instance.inStore = false;
    }

    public void UpdateGems()
    {
        //Debug.Log("Called");
        gemsText.SetText(Player.instance.gems.ToString());
    }

    private void OnTriggerStay2D(Collider2D other)
    {
       if (other.tag == "Player" && Input.GetKey(KeyCode.F))
        {
            Debug.Log("Open Shop Menu");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Player.instance.isPaused = true;
            Player.instance.inStore = true;
            shop.SetActive(true);
        }
    }
}
