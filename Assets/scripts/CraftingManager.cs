using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CraftingRecipe
{
    public Item resultItem;
    public Item[] requiredItems;
}

public class CraftingManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public InventorySlot[] craftingSlots; // 3 slots for crafting
    public InventorySlot resultSlot; // 1 slot for the result
    public CraftingRecipe[] recipes; // Array of crafting recipes
    public Button craftButton; // Button to initiate crafting
    public Transform player; // Reference to the player's transform

    private void Start()
    {
        craftButton.onClick.AddListener(CraftItem);
    }

    private void CraftItem()
    {
        if (resultSlot.transform.childCount > 0)
        {
            Debug.LogWarning("Result slot is not empty. Clear it before crafting another item.");
            return;
        }
        foreach (CraftingRecipe recipe in recipes)
        {
            if (IsRecipeMatch(recipe))
            {
                UseCraftingItems(recipe);
                DisplayResultItem(recipe.resultItem);
                Debug.Log("Crafted: " + recipe.resultItem.name);
                return;
            }
        }
        Debug.LogWarning("No matching recipe found.");
    }

    private bool IsRecipeMatch(CraftingRecipe recipe)
    {
        if (recipe.requiredItems.Length != craftingSlots.Length)
        {
            return false;
        }

        for (int i = 0; i < craftingSlots.Length; i++)
        {
            InventoryItem itemInSlot = craftingSlots[i].GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null || itemInSlot.item != recipe.requiredItems[i])
            {
                return false;
            }
        }

        return true;
    }

    private void UseCraftingItems(CraftingRecipe recipe)
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            InventoryItem itemInSlot = craftingSlots[i].GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                if (itemInSlot.item.stackable && itemInSlot.count > 1)
                {
                    itemInSlot.count--;
                    itemInSlot.RefreshCount();
                }
                else
                {
                    Destroy(itemInSlot.gameObject);
                }
            }
        }
    }

    private void DisplayResultItem(Item resultItem)
    {
        // Clear the previous result
        foreach (Transform child in resultSlot.transform)
        {
            Destroy(child.gameObject);
        }

        // Spawn new result item
        GameObject newItemGo = Instantiate(inventoryManager.InventoryItemPrefab, resultSlot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(resultItem);
    }
    public void TransferItemsToInventory()
    {
        // Transfer items from crafting slots to inventory
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            InventoryItem itemInSlot = craftingSlots[i].GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                for (int j = 0; j < itemInSlot.count; j++)
                {
                    if (!inventoryManager.AddItem(itemInSlot.item))
                    {
                        Debug.LogWarning("Inventory full, dropping item: " + itemInSlot.item.name);
                        DropItem(itemInSlot.item, itemInSlot.count);
                        break;
                    }
                }
                Destroy(itemInSlot.gameObject);
            }
        }

        // Transfer item from result slot to inventory
        InventoryItem resultItemInSlot = resultSlot.GetComponentInChildren<InventoryItem>();
        if (resultItemInSlot != null)
        {
            for (int j = 0; j < resultItemInSlot.count; j++)
            {
                if (!inventoryManager.AddItem(resultItemInSlot.item))
                {
                    Debug.LogWarning("Inventory full, dropping item: " + resultItemInSlot.item.name);
                    DropItem(resultItemInSlot.item, resultItemInSlot.count);
                    break;
                }
            }
            Destroy(resultItemInSlot.gameObject);
        }
    }

    private void DropItem(Item item, int count)
    {
        // Get the player's position and forward direction
        Vector3 dropPosition = player.position + player.forward;

        for (int i = 0; i < count; i++)
        {
            Instantiate(item.prefabToSpawn, dropPosition, Quaternion.identity);
        }
    }
}
