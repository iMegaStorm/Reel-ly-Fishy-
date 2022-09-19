using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateScript : MonoBehaviour
{
    [Header("Gate Info")]
    public GameObject gate;
    public GameObject gateVisual;
    public TextMeshProUGUI gateText;
    public int value = 0;
    private bool hidePrice;

    private void OnTriggerStay2D(Collider2D other)
    {
        //// If the trigger is the player and they have enough gold to pay the gate, then do the following
        //if (other.tag == "Player" && Player.instance.gems >= value)
        //{
        //    gateVisual.SetActive(true); // On screen visual for the price
        //    gateText.text = "Gate Price: " + value;
        //    // Whilst remaining in the trigger if the F key is pressed, they will pay the gate fee
        //    if (Input.GetKey(KeyCode.F))
        //    {
        //        gate.SetActive(false); // Gate becomes inactive
        //        gateVisual.SetActive(false);
        //        Player.instance.gems -= value; // Money is taken away from the player
        //    }
        //}
        //else if (other.tag == "Player" && Player.instance.gems < value)
        //{
        //    gateVisual.SetActive(true); // On screen visual for the price
        //    gateText.text = "Gate Price: " + value;

        //    if (Input.GetKey(KeyCode.F))
        //        gateText.text = "You need more Gems!";
        //}
        //else
        //    gateVisual.SetActive(false);

        if (other.tag == "Player")
        {
            if (!hidePrice)
            {
                gateVisual.SetActive(true); // On screen visual for the price
                gateText.text = "Gate Price: " + value;
                Debug.Log("Showing");
            }

            if (Player.instance.gems >= value)
            {
                if (Input.GetKey(KeyCode.F))
                {
                    gate.SetActive(false); // Gate becomes inactive
                    gateVisual.SetActive(false);
                    Player.instance.gems -= value; // Money is taken away from the player
                }
            }
            else if (Player.instance.gems < value && Input.GetKey(KeyCode.F))
            {
                gateText.text = "You need more Gems!";
                hidePrice = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gateVisual.SetActive(false);
            hidePrice = false;
        }
    }
}

