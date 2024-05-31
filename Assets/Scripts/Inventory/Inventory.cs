using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    private List<Item> itemList;

    public event EventHandler OnItemListChanged;
    public event Action<Item.ItemType, int> OnItemAdded; // Event for item added

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item(Item.ItemType.Crossbow, 1));
        AddItem(new Item(Item.ItemType.CopperGun, 1));
    }

    public void AddItem(Item item)
    {
        bool itemAlreadyInInventory = false;
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == item.itemType && inventoryItem.IsStackable())
            {
                inventoryItem.amount += item.amount;
                itemAlreadyInInventory = true;
                break;
            }
        }
        if (!itemAlreadyInInventory)
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        OnItemAdded?.Invoke(item.itemType, item.amount); // Trigger event when item is added
    }

    public void RemoveItem(Item item)
    {
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                if (inventoryItem.amount >= item.amount)
                {
                    inventoryItem.amount -= item.amount;
                    if (inventoryItem.amount <= 0)
                    {
                        itemList.Remove(inventoryItem);
                    }
                    OnItemListChanged?.Invoke(this, EventArgs.Empty);
                    break;
                }
            }
        }
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public int GetItemAmount(Item.ItemType itemType)
    {
        foreach (Item item in itemList)
        {
            if (item.itemType == itemType)
            {
                return item.amount;
            }
        }
        return 0;
    }
}