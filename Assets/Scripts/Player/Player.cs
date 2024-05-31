using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Animation variables
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    // Health variables
    public PlayerHealth playerHealth;
    public Health healthBar;

    // Infected status variables
    public PlayerInfection playerInfection;
    public Infection infectionBar;

    // Inventory variables
    public Inventory inventory;
    [SerializeField] private UI_Inventory inventoryUI;

    // Speed variables
    [SerializeField] private float baseSpeed = 10f;
    [SerializeField] private float maxInfectionSpeedMultiplier = 0.5f;
    [SerializeField] private float minInfectionSpeedMultiplier = 1.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        inventory = new Inventory();
        if (inventoryUI == null)
        {
            Debug.LogError("Inventory UI is not assigned.");
        }
        else
        {
            Debug.Log("Setting inventory in UI");
            inventoryUI.SetInventory(inventory, playerHealth.currentHealth, playerHealth.maxHealth, playerInfection.currentInfection, playerInfection.maxInfection, healthBar, infectionBar, this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            Item item = itemWorld.GetItem();
            if (item == null)
            {
                Debug.LogError("ItemWorld returned a null item.");
                return;
            }

            inventory.AddItem(item);
            itemWorld.DestroySelf();
        }
    }

    public void OnMovement(InputValue value)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("DyingStateTree"))
        {
            movement = value.Get<Vector2>();

            if (movement.x != 0 || movement.y != 0)
            {
                animator.SetFloat("X", movement.x);
                animator.SetFloat("Y", movement.y);
                animator.SetBool("IsWalking", true);
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
        }
        else
        {
            movement = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        float speedMultiplier = Mathf.Lerp(minInfectionSpeedMultiplier, maxInfectionSpeedMultiplier, (float)playerInfection.currentInfection / playerInfection.maxInfection);
        float currentSpeed = baseSpeed * speedMultiplier;

        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }
}
