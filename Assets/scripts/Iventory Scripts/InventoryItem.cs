using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    [Header("UI")]
    public Image image;
    public Text countText;

    public Item item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;
    /*private int initialCount; // Store initial count when dragging starts
    private int maxStackSize; // Maximum stack size defined in InventoryManager

    private void Awake()
    {
        initialCount = count;
        maxStackSize = InventoryManager.instance.maxStackedItems;
    }*/

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Draging detected!");
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    /*public void OnScroll(PointerEventData eventData)
    {
        Debug.Log("Scrolling detected!");
        float scrollDelta = eventData.scrollDelta.y;
        int increment = (scrollDelta > 0) ? 1 : -1; // Determine whether to increase or decrease count
        count = Mathf.Clamp(initialCount + increment, 1, maxStackSize); // Ensure count stays within bounds
        RefreshCount();
    }*/
}
