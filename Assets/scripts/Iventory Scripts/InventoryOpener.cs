using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOpener : MonoBehaviour
{
    // Reference to the inventory UI game object
    public GameObject inventoryUI;
    public GameObject darkBackground;
    public GameObject crafting;

    public PlayerController playerController;
    public CraftingManager craftingManager; // Add a reference to the CraftingManager
    // Boolean flag to track if the inventory is open
    public bool isInventoryOpen = false;

    // Update is called once per frame
    void Update()
    {
        // Check if the "I" key is pressed
        if (Input.GetKeyDown(KeyCode.I))
        {
            playerController.enabled = !playerController.enabled;
            // Toggle the inventory UI visibility based on its current state
            if (isInventoryOpen)
            {
                CloseInventory();
                
            }
            else
            {
                OpenInventory();
                
            }
        }
    }

    // Method to open the inventory UI
    void OpenInventory()
    {
        inventoryUI.SetActive(true);
        darkBackground.SetActive(true);
        crafting.SetActive(true);
        isInventoryOpen = true;
        Cursor.lockState = CursorLockMode.Confined;

    }

    // Method to close the inventory UI
    void CloseInventory()
    {
        inventoryUI.SetActive(false);
        darkBackground.SetActive(false);
        crafting.SetActive(false);
        isInventoryOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        craftingManager.TransferItemsToInventory();
    }
}
