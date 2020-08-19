using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    Item slotItem;
    public Image icon;

    public void AddItem(Item newItem)
    {
        slotItem = newItem;
        icon.sprite = newItem.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        slotItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
