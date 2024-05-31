using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private AudioClip itemPickupSound;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        if (item != null)
        {
            item.SetEffectsForItem(item.itemType);
            SetItem(item);
        }
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
        if (item != null)
        {
            item.SetEffectsForItem(item.itemType); 
            if (spriteRenderer != null)
            {
                Sprite itemSprite = item.GetSprite();
                if (itemSprite != null)
                {
                    spriteRenderer.sprite = itemSprite;
                }
                else
                {
                    Debug.LogError("Failed to get sprite from item: " + item.itemType);
                }
            }
            else
            {
                Debug.LogError("SpriteRenderer component is missing on ItemWorld GameObject");
            }
        }
        else
        {
            Debug.LogError("newItem is null in SetItem method");
        }
    }

    private void OnValidate()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        SetItem(item);
    }

    // Method to get the item of this ItemWorld instance
    public Item GetItem()
    {
        SoundManager.instance.PlaySoundFX(itemPickupSound, transform.position, 1f);
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Logic to pick up the item
            Item pickedItem = GetItem();
            DestroySelf();
        }
    }
}