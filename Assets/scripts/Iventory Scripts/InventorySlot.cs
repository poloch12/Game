using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;
    public int index;

    private void Awake()
    {
        DeSelect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }
    public void DeSelect()
    {
        image.color = notSelectedColor;
    }
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem draggedItem = eventData.pointerDrag.GetComponent<InventoryItem>();


        if (transform.childCount == 0) // Slot is empty
        {
            if (draggedItem != null)
            {
                draggedItem.parentAfterDrag = transform;
                draggedItem.transform.SetParent(transform);
            }
        }
        else // Slot is not empty
        {
            InventoryItem currentItem = GetComponentInChildren<InventoryItem>();

            if (currentItem != null && draggedItem != null && currentItem.item == draggedItem.item && currentItem.item.stackable)
            {
                int totalCount = currentItem.count + draggedItem.count;
                if (totalCount <= currentItem.item.maxStackCount)
                {
                    currentItem.count = totalCount;
                    currentItem.RefreshCount();
                    Destroy(draggedItem.gameObject); // Destroy the dragged item as it has been stacked
                }
                else
                {
                    int remainingCount = currentItem.item.maxStackCount - currentItem.count;
                    currentItem.count = currentItem.item.maxStackCount;
                    currentItem.RefreshCount();

                    draggedItem.count -= remainingCount;
                    draggedItem.RefreshCount();
                }
            }
        }
    }
}
