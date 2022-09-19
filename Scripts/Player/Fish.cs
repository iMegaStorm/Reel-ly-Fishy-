using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Fish
{
    public enum FishType
    {
        Anchovy,
        Carp,
        Mackerel,
        Pike,
        Redfish,
        Sea_Bass,
        Trout,
        Whiting,
    }

    public FishType fishType;
    public int amount;
    public int value;

    public Sprite GetSprite()
    {
        switch (fishType)
        {
            default:
            case FishType.Anchovy: 
                return FishAssets.instance.AnchovySprite;
            case FishType.Carp: 
                return FishAssets.instance.CarpSprite;
            case FishType.Mackerel: 
                return FishAssets.instance.MackerelSprite;
            case FishType.Pike: 
                return FishAssets.instance.PikeSprite;
            case FishType.Redfish: 
                return FishAssets.instance.RedfishSprite;
            case FishType.Sea_Bass: 
                return FishAssets.instance.Sea_BassSprite;
            case FishType.Trout: 
                return FishAssets.instance.TroutSprite;
            case FishType.Whiting: 
                return FishAssets.instance.WhitingSprite;

        }
    }
    public bool IsStackable()
    {
        switch (fishType)
        {
            default:
            case FishType.Anchovy:
            case FishType.Carp:
            case FishType.Mackerel:
            case FishType.Pike:
            case FishType.Redfish:
            case FishType.Sea_Bass:
            case FishType.Trout:
                return true;
            case FishType.Whiting:
                return false;
        }
    }
}
