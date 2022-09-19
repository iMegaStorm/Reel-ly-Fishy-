using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Shop[] shops;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Q))
        {
            int cheatGiveGems = Random.Range(50,500);
            player.gems += cheatGiveGems;

            for (int x = 0; x < shops.Length; x++)
                shops[x].UpdateGems();

            Debug.Log("Cheat: " + cheatGiveGems + " Money Given to Player");
        }
    }
}
