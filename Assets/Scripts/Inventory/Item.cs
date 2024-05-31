using UnityEngine;

[System.Serializable]
public class Item
{
    public enum ItemType
    {
        Crossbow,
        CopperGun,
        CopperBullets,
        TunaCan,
        Sandwich,
        Chips,
        Burrito,
    }

    public ItemType itemType;
    public int amount;

    public int healthEffect;
    public int infectionEffect;

    public Item(ItemType itemType, int amount)
    {
        this.itemType = itemType;
        this.amount = amount;
        SetEffectsForItem(itemType); // Set health and infection effects based on item type
    }

    // Function to set health and infection effects based on item type
    public void SetEffectsForItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Crossbow:
                healthEffect = 0;
                infectionEffect = 0;
                break;
            case ItemType.CopperGun:
                healthEffect = 0;
                infectionEffect = 0;
                break;
            case ItemType.CopperBullets:
                healthEffect = 0;
                infectionEffect = 0;
                break;
            case ItemType.TunaCan:
                healthEffect = 20;
                infectionEffect = -5;
                break;
            case ItemType.Sandwich:
                healthEffect = 15;
                infectionEffect = 10;
                break;
            case ItemType.Chips:
                healthEffect = 10;
                infectionEffect = 5;
                break;
            case ItemType.Burrito:
                healthEffect = -25;
                infectionEffect = 35;
                break;
            default:
                healthEffect = 0;
                infectionEffect = 0;
                break;
        }
    }

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Crossbow: return ItemAssets.Instance.crossbowSprite;
            case ItemType.CopperGun: return ItemAssets.Instance.copperGunSprite;
            case ItemType.CopperBullets: return ItemAssets.Instance.copperBulletsSprite;
            case ItemType.TunaCan: return ItemAssets.Instance.tunaCanSprite;
            case ItemType.Sandwich: return ItemAssets.Instance.sandwichSprite;
            case ItemType.Chips: return ItemAssets.Instance.chipsSprite;
            case ItemType.Burrito: return ItemAssets.Instance.burittoSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.CopperGun:
            case ItemType.Crossbow:
                return false;
            case ItemType.CopperBullets:
            case ItemType.TunaCan:
            case ItemType.Sandwich:
            case ItemType.Chips:
            case ItemType.Burrito:
                return true;
        }
    }

    public bool IsConsumable()
    {
        switch (itemType)
        {
            case ItemType.TunaCan:
            case ItemType.Sandwich:
            case ItemType.Chips:
            case ItemType.Burrito:
                return true;
            default:
                return false;
        }
    }
}