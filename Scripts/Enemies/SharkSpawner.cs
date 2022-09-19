using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    //[HideInInspector]
    public GameObject shark;

    [Header("Spawner Info")]
    public float spawnCheckTime;
    public float spawnRadius;
    public int maxRandomNumber;
    public int maxSharks;
    public bool isTriggered;
    private float lastSpawnCheckTime;

    [Header("Fish Spawner List")]
    public List<GameObject> sharkList = new List<GameObject>();

    [Header("Debug Mode")]
    public bool spawnRange = false;

    void Update()
    {
        if (isTriggered)
        {
            if (Time.time - lastSpawnCheckTime > spawnCheckTime)
                if (sharkList.Count < maxSharks)
                {
                    lastSpawnCheckTime = Time.time;

                    TrySpawn();
                }

            // remove any dead enemies from the curEnemies list
            for (int x = 0; x < sharkList.Count; ++x)
                if (!sharkList[x])
                    sharkList.RemoveAt(x);
        }

        void TrySpawn()
        {
            // remove any dead enemies from the curEnemies list
            for (int x = 0; x < sharkList.Count; ++x)
                if (!sharkList[x])
                    sharkList.RemoveAt(x);

            if (sharkList.Count >= maxSharks)
                return;


            // otherwise spawn an enemy
            var number = Random.Range(1, maxRandomNumber);
            Vector3 randomInCircle = Random.insideUnitCircle * spawnRadius;

            if (number > 4 && number < 6)   //10%
            {
                Instantiate(shark, transform.position + randomInCircle, Quaternion.identity);
                sharkList.Add(shark);
            }
            else
                Debug.Log("Failed to Spawn: " + number);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            isTriggered = true;
    }

    void OnDrawGizmosSelected()
    {
        if (spawnRange)
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, spawnRadius);
        }
    }
}
