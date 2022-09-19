using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishSpawner : MonoBehaviour
{
    [HideInInspector]
    public GameObject anchovy;
    [HideInInspector]
    public GameObject pike;
    [HideInInspector]
    public GameObject redFish;
    [HideInInspector]
    public GameObject carp;
    [HideInInspector]
    public GameObject mackerel;
    [HideInInspector]
    public GameObject whiting;
    [HideInInspector]
    public GameObject trout;
    [HideInInspector]
    public GameObject seaBass;

    [Header("Spawner Info")]
    public float spawnCheckTime;
    public float spawnRadius;
    public int maxRandomNumber;
    public int maxFishes;
    public bool isTriggered;
    private float lastSpawnCheckTime;

    [Header("Specific Fish Spawns")]
    public bool pikeSpawn;
    public bool mackerelSpawn;
    public bool troutSpawn;
    public bool whitingSpawn;

    [Header("Fish Spawner List")]
    public List<GameObject> fishList = new List<GameObject>();

    //[Header("Fish Spawn Points")]
    //public GameObject[] fishSpawns;

    //[Header("Type Of Fish")]
    //public GameObject[] fishType;

    [Header("Debug Mode")]
    public bool spawnRange = false;

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            if (Time.time - lastSpawnCheckTime > spawnCheckTime)
                if (fishList.Count < maxFishes)
                {
                    lastSpawnCheckTime = Time.time;

                    TrySpawn();
                }

            // remove any dead enemies from the curEnemies list
            for (int x = 0; x < fishList.Count; ++x)
            {
                if (!fishList[x])
                {
                    fishList.RemoveAt(x);
                    //enemies -= 1;
                }
            }
        }


    }

    void TrySpawn()
    {
        // remove any dead enemies from the curEnemies list
        for (int x = 0; x < fishList.Count; ++x)
        {
            if (!fishList[x])
            {
                fishList.RemoveAt(x);
                //enemies -= 1;
            }
        }

        if (fishList.Count >= maxFishes)
            return;


        // otherwise spawn an enemy
        var number = Random.Range(1, maxRandomNumber);
        Vector3 randomInCircle = Random.insideUnitCircle * spawnRadius;

        if (number >= 1 && number <= 20)                                                    // 1  - 20  - 20%
        {
            GameObject anchovyFish = Instantiate(anchovy, transform.position + randomInCircle, Quaternion.identity);
            fishList.Add(anchovyFish);
        }
        else if (number >= 21 && number <= 35)                                              // 21 - 35  - 15%
        {
            GameObject carpFish = Instantiate(carp, transform.position + randomInCircle, Quaternion.identity);
            fishList.Add(carpFish);
        }
        else if (number >= 36 && number <= 50 && mackerelSpawn)                             // 36 - 50  - 15%
        {
            GameObject mackerelFish = Instantiate(mackerel, transform.position + randomInCircle, Quaternion.identity);
            fishList.Add(mackerelFish);
        }
        else if (number >= 51 && number <= 65 && pikeSpawn)                                 // 51 - 65  - 15%
        {
            GameObject pikeFish = Instantiate(pike, transform.position + randomInCircle, Quaternion.identity);
            fishList.Add(pikeFish);
        }
        else if (number >= 66 && number <= 75)                                              // 66 - 75  - 10%
        {
            GameObject redFishObj = Instantiate(redFish, transform.position + randomInCircle, Quaternion.identity);
            fishList.Add(redFishObj);
        }
        else if (number >= 76 && number <= 85)                                              // 76 - 85  - 10%
        {
            GameObject seaBassFish = Instantiate(seaBass, transform.position + randomInCircle, Quaternion.identity);
            fishList.Add(seaBassFish);
        }
        else if (number >= 86 && number <= 95 && troutSpawn)                                // 86 - 95  - 10%
        {
            GameObject troutFish = Instantiate(trout, transform.position + randomInCircle, Quaternion.identity);
            fishList.Add(troutFish);
        }
        else if (number >= 96 && number <= 100 && whitingSpawn)                             // 96 - 100 - 5%
        {
            GameObject whitingFish = Instantiate(whiting, transform.position + randomInCircle, Quaternion.identity);
            fishList.Add(whitingFish);
        }
        //Debug.Log(Random.Range(1, maxRandomNumber));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTriggered = true;
            //Debug.Log("Enter Trigger!");
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTriggered = false;
            Debug.Log("Exit Trigger!");
        }
    }

    void OnDrawGizmosSelected()
    {
        if(spawnRange)
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, spawnRadius);
        }
    }
}

// OLD CODE
//IEnumerator EnemySpawner()
//{
//    while (fishCount < )
//    {
//        var number = Random.Range(1, maxRandomNumber);

//        if (number >= 1 && number <= 20)            // 1  - 20  - 20%
//            Instantiate(anchovy, new Vector3(fishSpawns[fishCount].transform.position.x, fishSpawns[fishCount].transform.position.y, 0), Quaternion.identity);
//        else if (number >= 21 && number <= 35)      // 21 - 35  - 15%
//            Instantiate(pike, new Vector3(fishSpawns[fishCount].transform.position.x, fishSpawns[fishCount].transform.position.y, 0), Quaternion.identity);
//        else if (number >= 36 && number <= 50)      // 36 - 50  - 15%
//            Instantiate(redFish, new Vector3(fishSpawns[fishCount].transform.position.x, fishSpawns[fishCount].transform.position.y, 0), Quaternion.identity);
//        else if (number >= 51 && number <= 65)      // 51 - 65  - 15%
//            Instantiate(carp, new Vector3(fishSpawns[fishCount].transform.position.x, fishSpawns[fishCount].transform.position.y, 0), Quaternion.identity);
//        else if (number >= 66 && number <= 75)      // 66 - 75  - 10%
//            Instantiate(mackerel, new Vector3(fishSpawns[fishCount].transform.position.x, fishSpawns[fishCount].transform.position.y, 0), Quaternion.identity);
//        else if (number >= 76 && number <= 85)      // 76 - 85  - 10%
//            Instantiate(whiting, new Vector3(fishSpawns[fishCount].transform.position.x, fishSpawns[fishCount].transform.position.y, 0), Quaternion.identity);
//        else if (number >= 86 && number <= 95)      // 86 - 95  - 10%
//            Instantiate(trout, new Vector3(fishSpawns[fishCount].transform.position.x, fishSpawns[fishCount].transform.position.y, 0), Quaternion.identity);
//        else if (number >= 96 && number <= 100)     // 96 - 100 - 5%
//            Instantiate(seaBass, new Vector3(fishSpawns[fishCount].transform.position.x, fishSpawns[fishCount].transform.position.y, 0), Quaternion.identity);

//        fishCount += 1;
//        yield return new WaitForSeconds(0.5f);           
//    }
//}

//private void OnTriggerEnter2D(Collider2D other)
//{
//    if (other.gameObject.tag == "Player")
//    {
//        //Debug.Log("Poopin Fishes");
//        StartCoroutine(EnemySpawner());
//    }
//}
