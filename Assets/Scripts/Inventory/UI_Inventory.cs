using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Dictionary<Item.ItemType, int> itemCounts = new Dictionary<Item.ItemType, int>();

    // Health variables
    private int currentHealth;
    private int maxHealth;
    private int currentInfection;
    private int maxInfection;
    private Health healthBar;
    private Infection infectionBar;
    private Player player;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        if (itemSlotContainer == null)
        {
            Debug.LogError("ItemSlotContainer not found!");
            return;
        }

        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
        if (itemSlotTemplate == null)
        {
            Debug.LogError("ItemSlotTemplate not found!");
            return;
        }
    }

    public void SetInventory(Inventory inventory, int initialHealth, int maxHealth, int initialInfection, int maxInfection, Health healthBar, Infection infectionBar, Player player)
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory is null.");
            return;
        }

        this.inventory = inventory;
        this.currentHealth = initialHealth;
        this.maxHealth = maxHealth;
        this.currentInfection = initialInfection;
        this.maxInfection = maxInfection;
        this.healthBar = healthBar;
        this.infectionBar = infectionBar;
        this.player = player;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        if (itemSlotContainer == null || itemSlotTemplate == null)
        {
            Debug.LogError("ItemSlotContainer or ItemSlotTemplate is not set.");
            return;
        }

        if (inventory == null)
        {
            Debug.LogError("Inventory is not set.");
            return;
        }

        var itemList = inventory.GetItemList();
        if (itemList == null)
        {
            Debug.LogError("Inventory item list is not set.");
            return;
        }

        // Clear existing counts before refreshing
        itemCounts.Clear();

        // Calculate item counts in inventory
        foreach (Item item in itemList)
        {
            if (item == null)
            {
                Debug.LogError("Item is null.");
                continue;
            }

            if (itemCounts.ContainsKey(item.itemType))
            {
                itemCounts[item.itemType] += item.amount;
            }
            else
            {
                itemCounts[item.itemType] = item.amount;
            }
        }

        // Clear existing UI slots
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int maxItemsPerRow = 7;
        float itemSlotCellSize = 120f;

        // Create new UI slots based on item counts
        foreach (var itemType in itemCounts.Keys)
        {
            if (x >= maxItemsPerRow)
            {
                break;
            }

            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, 0);

            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            if (image == null)
            {
                Debug.LogError("Image component not found in ItemSlotTemplate.");
                continue;
            }

            var itemSprite = itemList.Find(i => i.itemType == itemType)?.GetSprite();
            if (itemSprite != null)
            {
                image.sprite = itemSprite;
            }

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>();
            if (itemCounts[itemType] > 1)
            {
                uiText.SetText(itemCounts[itemType].ToString());
            }
            else
            {
                uiText.SetText("");
            }

            // Add event listener for consume action
            ItemSlot itemSlot = itemSlotRectTransform.GetComponent<ItemSlot>();
            var item = itemList.Find(i => i.itemType == itemType);
            itemSlot.SetItem(item); 
            if (item != null && item.IsConsumable())
            {
                SubscribeToRightClick(itemSlot, item);
            }

            x++;
        }
    }

    private void SubscribeToRightClick(ItemSlot itemSlot, Item item)
    {
        itemSlot.OnRightClick += () => ConsumeItem(item);
    }

    public void ConsumeItem(Item item)
    {
        if (item.IsConsumable())
        {
            player.playerHealth.Heal(item.healthEffect);
            player.playerInfection.Infect(item.infectionEffect);

            if (item.IsStackable() && item.amount > 1)
            {
                item.amount--;
            }
            else
            {
                inventory.RemoveItem(item); // Remove the item from the inventory
            }

            RefreshInventoryItems();
        }
        else
        {
            Debug.Log("Item cannot be consumed: " + item.itemType);
        }
    }
}