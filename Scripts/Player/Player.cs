using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int curHealth;
    public int maxHealth;
    public int gems;
    public int curFish;
    public int maxFish;

    [Header("Movement")]
    [SerializeField]
    public float accelerationPower = 5f;
    [SerializeField]
    float steeringPower = 5f;
    float steeringAmount, speed, direction;
    public bool ableToMove = true;
    public bool fishInRange;
    public bool fishing = false;
    public bool isPaused = false;

    [Header("UI")]
    public bool inStore;

    [Header("Fishing")]
    public float fishingTimer = 2f;
    public float defaultFishingTimer = 2f;
    public bool fishOnHook = false;
    public GameObject fishingHook;
    public GameObject fishingRod;

    [Header("Audio")]
    public AudioSource adSource;
    public AudioClip failedFishingRodCast;
    public AudioClip[] adClips;

    [Header("Components")]
    public Collider2D baitCollider;
    public Collider2D hookCollider;
    public Inventory inventory;
    public GameObject inventoryMenu;
    private Rigidbody2D rig;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private UI_StoreInventory uiStoreInventory;

    public GameObject inventoryCanvas;

    public static Player instance;

    private void Awake()
    {
        // set the instance to this script
        instance = this;

        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        uiStoreInventory.SetInventory(inventory);
    }

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        //fishingHook.transform.localScale = new Vector2(0, 0);
        fishingRod = GameObject.Find("FishingRod"); 
        fishingHook = GameObject.Find("Hook");
        //fish = GetComponent<Fish>();
        //fishingRod.transform.localScale = new Vector2(0, 0);
        fishingRod.SetActive(false);
        fishingHook.SetActive(false);

        fishInRange = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false)
        {
            if (Input.GetMouseButton(0) && curFish < maxFish)
                StartFishing();
            else if (Input.GetMouseButton(1) || curFish >= maxFish)
                CancelFishing();
        }

        if (Input.GetKey(KeyCode.Alpha1))
            inventoryCanvas.SetActive(true);

        if (fishingTimer > 0.0f && fishing)
            fishingTimer -= Time.deltaTime;
        else if (fishingTimer <= 0f && !fishOnHook)
            baitCollider.enabled = true;
    }

    public void StartFishing()
    {
        ableToMove = false;
        fishing = true;
        fishingRod.SetActive(true);
        fishingHook.SetActive(true);
        //fishingRod.transform.localScale = new Vector2(0.5f, 0.05f);
        //fishingHook.transform.localScale = new Vector2(0.1f, 0.1f);
        //fishingHook.SetActive(true);
        //Debug.Log(fishingTimer);
    }

    public void CancelFishing()
    {
        ableToMove = true;
        fishing = false;
        baitCollider.enabled = false;
        hookCollider.enabled = false;
        fishInRange = false;
        fishingRod.SetActive(false);
        fishingHook.SetActive(false);

        //fishingRod.transform.localScale = new Vector2(0, 0);
        //fishingHook.transform.localScale = new Vector2(0, 0);
        //fishingHook.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (ableToMove && isPaused == false)
        {
            steeringAmount = -Input.GetAxis("Horizontal");
            speed = Input.GetAxis("Vertical") * accelerationPower;
            direction = Mathf.Sign(Vector2.Dot(rig.velocity, rig.GetRelativeVector(Vector2.up)));
            rig.rotation += steeringAmount * steeringPower * rig.velocity.magnitude * direction;

            rig.AddRelativeForce(Vector2.up * speed);

            rig.AddRelativeForce(-Vector2.right * rig.velocity.magnitude * steeringAmount / 2);
        }
    }
}

// OLD CODE
//public void InventoryToggle()
//{
//    isPaused = !isPaused;       
//}

//void InventoryMenu()
//{
//    if (isPaused && !inStore)
//        inventoryMenu.SetActive(true);
//    else
//        inventoryMenu.SetActive(false);
//}
