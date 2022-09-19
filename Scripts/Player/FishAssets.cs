using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAssets : MonoBehaviour
{
    public static FishAssets instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public Sprite AnchovySprite;
    public Sprite CarpSprite;
    public Sprite MackerelSprite;
    public Sprite PikeSprite;
    public Sprite RedfishSprite;
    public Sprite Sea_BassSprite;
    public Sprite TroutSprite;
    public Sprite WhitingSprite;
}
