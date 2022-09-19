using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    public List<Fish> fishList;

    public Inventory()
    {
        fishList = new List<Fish>();
        //AddFish(new Fish { fishType = Fish.FishType.Anchovy, amount = 18, value = 25 });
        //AddFish(new Fish { fishType = Fish.FishType.Carp, amount = 1, value = 35 });
        //AddFish(new Fish { fishType = Fish.FishType.Mackerel, amount = 1, value = 50 });
        //AddFish(new Fish { fishType = Fish.FishType.Pike, amount = 1, value = 55 });
        //AddFish(new Fish { fishType = Fish.FishType.Redfish, amount = 1, value = 75 });
        //AddFish(new Fish { fishType = Fish.FishType.Sea_Bass, amount = 1, value = 100 });
        //AddFish(new Fish { fishType = Fish.FishType.Trout, amount = 1, value = 125 });
        //AddFish(new Fish { fishType = Fish.FishType.Whiting, amount = 1, value = 150 });
        //AddFish(new Fish { fishType = Fish.FishType.Whiting, amount = 1, value = 150 });
        //AddFish(new Fish { fishType = Fish.FishType.Whiting, amount = 1, value = 150 });

        //AddFish(new Fish { fishType = Fish.FishType.Carp, amount = 1 });
        //AddFish(new Fish { fishType = Fish.FishType.Pike, amount = 1 });
        //AddFish(new Fish { fishType = Fish.FishType.Pike, amount = 1 });
        //AddFish(new Fish { fishType = Fish.FishType.Pike, amount = 1 });

        //Debug.Log(fishList.Count);
    }

    public void AddFish(Fish fish)
    {
        if (fish.IsStackable())
        {
            bool fishAlreadyInInventory = false;
            foreach (Fish inventoryItem in fishList)
                if (inventoryItem.fishType == fish.fishType)
                {
                    inventoryItem.amount += fish.amount;
                    inventoryItem.value += fish.value;
                    fishAlreadyInInventory = true;
                }

            // Add the fish if there isnt one already in the inventory
            if (!fishAlreadyInInventory)
                fishList.Add(fish);
        }
        else
            fishList.Add(fish); // If the fish isnt stackable then add the fish

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Fish> GetItemList()
    {
        return fishList;
    }

    public void RemoveFish(Fish fish)
    {
            fishList.Clear();
    }

}

//[Header("Data")]
//public int fishWorth = 0;

//public static Inventory instance;

//private void Awake()
//{
//    instance = this;
//}

//public void UpdateFishWorth(int value)
//{
//    // Takes the value of the fish and adds it to the inventories fish worth
//    fishWorth += value;

//    //Debug.Log("Fish Value: " + value);
//    //Debug.Log("Fish Worth: " + fishWorth);
//}
