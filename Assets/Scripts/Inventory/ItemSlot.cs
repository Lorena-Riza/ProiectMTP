using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private Item item;
    private Shooting shooting;

    public delegate void OnItemClicked();
    public event OnItemClicked OnLeftClick;
    public event OnItemClicked OnRightClick;

    private void Start()
    {
        shooting = FindObjectOfType<Shooting>(); // Find the Shooting script in the scene
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick?.Invoke();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick?.Invoke();
            HandleRightClick();
        }
    }

    void HandleRightClick()
    {
        if (shooting != null && item != null)
        {
            if (item.itemType == Item.ItemType.Crossbow || item.itemType == Item.ItemType.CopperGun)
            {
                shooting.SetSelectedProjectileType(item.itemType);
            }
        }
    }

    public Item GetItem()
    {
        return item;
    }
}