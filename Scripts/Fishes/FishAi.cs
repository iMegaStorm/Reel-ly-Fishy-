using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class FishAi : MonoBehaviour
{
    [Header("Fish Data")]
    public int ID;
    public int catchChance;
    public int maxRange;
    public bool hasBeenUpgraded;

    [Header("Movement")]
    [SerializeField]
    float speed;
    [SerializeField]
    float range = 0.5f;
    [SerializeField]
    float maxDistance;
    public bool ableToMove = true;
    public bool goingToHook = false;

    [Header("Components")]
    public GameObject fishHook;
    Vector2 wayPoint;
    Vector3 target;

    [Header("Debug Mode")]
    public bool toggle;

    private void Awake()
    {
        wayPoint = this.transform.position;
        wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }

    void Update()
    {
        // Debug mode
        if (Input.GetKeyDown(KeyCode.P))
            toggle = !toggle;
        
        // get the angle
        Vector3 norTar = (target - transform.position).normalized;
        float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;

        // rotate to angle
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle - 90);
        transform.rotation = rotation;

        if (ableToMove)
        {
            if (Vector2.Distance(transform.position, wayPoint) < range && !goingToHook)
            {
                wayPoint = new Vector2(Random.Range(transform.position.x -maxDistance, transform.position.x +maxDistance), Random.Range(transform.position.y -maxDistance, transform.position.y +maxDistance));
                target = wayPoint;
                //Debug.Log(wayPoint);

            }
            else if (goingToHook)
            {
                target = fishHook.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, fishHook.transform.position, speed * Time.deltaTime);
            }
            else
            {
                target = wayPoint;
                transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
            }
        }

        FishUpgrade();
    }

    void FishUpgrade()
    {
        if (UpgradeScript.instance.catchRateUpgrade == true)
        {
            if (!hasBeenUpgraded)
            {
                catchChance += 10;
                hasBeenUpgraded = true;
            }
        }
    }

    void CatchChance()
    {
        var number = Random.Range(1, maxRange);

        if (!AudioManager.instance.audio.isPlaying)
            AudioManager.instance.audio.PlayOneShot(AudioManager.instance.fishBite);

        if (number <= catchChance)
        {
            FishingTextUI.instance.fishCaught = true;
            Player.instance.curFish += 1;
            AddFish();
            Destroy(gameObject);
            Debug.Log("Caught: " + number);
        }
        else
        {
            FishingTextUI.instance.fishFled = true;
            Destroy(gameObject);
            Debug.Log("Fled: " + number);
        }
    }

    void AddFish()
    {
        if (ID == 1)
            Player.instance.inventory.AddFish(new Fish { fishType = Fish.FishType.Anchovy, amount = 1, value = 50}); 
        else if (ID == 2)
            Player.instance.inventory.AddFish(new Fish { fishType = Fish.FishType.Carp, amount = 1, value = 70 });
        else if (ID == 3)
            Player.instance.inventory.AddFish(new Fish { fishType = Fish.FishType.Mackerel, amount = 1, value = 100 });
        else if (ID == 4)
            Player.instance.inventory.AddFish(new Fish { fishType = Fish.FishType.Pike, amount = 1, value = 110 });
        else if (ID == 5)
            Player.instance.inventory.AddFish(new Fish { fishType = Fish.FishType.Redfish, amount = 1, value = 150 });
        else if (ID == 6)
            Player.instance.inventory.AddFish(new Fish { fishType = Fish.FishType.Sea_Bass, amount = 1, value = 200 });
        else if (ID == 7)
            Player.instance.inventory.AddFish(new Fish { fishType = Fish.FishType.Trout, amount = 1, value = 250 });
        else if (ID == 8)
            Player.instance.inventory.AddFish(new Fish { fishType = Fish.FishType.Whiting, amount = 1, value = 300 });
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!goingToHook)
        {
            wayPoint = new Vector2(Random.Range(transform.position.x - maxDistance, transform.position.x + maxDistance), Random.Range(transform.position.y - maxDistance, transform.position.y + maxDistance));
            target = wayPoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "HookRange")
        {
            fishHook = GameObject.Find("Hook");
            Player.instance.hookCollider.enabled = true;
            goingToHook = true;
        }

        if (other.gameObject.tag == "Hook")
        {
            Player.instance.fishOnHook = true;
            Player.instance.fishingTimer = Player.instance.defaultFishingTimer;

            CatchChance();
            Player.instance.CancelFishing();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "HookRange")
        {
            goingToHook = false;

        }

        if (other.tag == "Hook")
            Player.instance.fishOnHook = false;
    }

    private void OnDrawGizmos()
    {
        if (toggle)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, range);
        }
        else
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(wayPoint, 1);

        }
    }
}

// ~~~~~~~ <>< ~~~~~~~
