using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingTextUI : MonoBehaviour
{
    [Header("UI Text")]
    public GameObject fishCaughtText;
    public GameObject fishFledText;
    public GameObject fullInventText;
    public float textTimer = 2.5f;
    public bool fishCaught;
    public bool fishFled;

    public static FishingTextUI instance;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (fishCaught || fishFled)
        {
            if (textTimer > 0)
                textTimer -= Time.deltaTime;
            else if (textTimer <= 0)
            {
                fishCaught = false;
                fishFled = false;
                textTimer = 2.5f;
            }
        }
        
        if (fishCaught)
            FishCaught();
        else
            fishCaughtText.SetActive(false);

        if (fishFled)
            FishFled();
        else
            fishFledText.SetActive(false);

        if (Player.instance.curFish >= Player.instance.maxFish)
            FullInventory();
        else
            fullInventText.SetActive(false);



    }

    void FishCaught()
    {
        fishCaughtText.SetActive(true);
    }

    void FishFled()
    {
        fishFledText.SetActive(true);
    }

    void FullInventory()
    {
        fullInventText.SetActive(true);
    }
}
