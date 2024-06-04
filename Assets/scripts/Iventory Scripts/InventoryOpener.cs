using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOpener : MonoBehaviour
{
    // Reference to the inventory UI game object
    public GameObject inventoryUI;
    public GameObject darkBackground;
    public GameObject crafting;
    public GameObject campfireUI;
    public GameObject chestUI;

    public float interactionDistance = 5f;
    public PlayerController playerController;
    public CraftingManager craftingManager; // Add a reference to the CraftingManager
    // Boolean flag to track if the inventory is open
    public bool isInventoryOpen = false;
    public bool isCampfireOpen = false;
    public bool isChestOpen = false;

    // Update is called once per frame
    void Update()
    {
        // Check if the "I" key is pressed
        if (Input.GetKeyDown(KeyCode.I) && !isCampfireOpen && !isChestOpen)
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isInventoryOpen)
            {
                TryOpenCampfireUI();
                TryOpenCestUI();
            }
            else if(isCampfireOpen)
            {
                CloseCampfireUI();
            }
            else
            {
                CloseChestUI();
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
        Cursor.visible = true;
    }

    // Method to close the inventory UI
    void CloseInventory()
    {
        inventoryUI.SetActive(false);
        darkBackground.SetActive(false);
        crafting.SetActive(false);
        isInventoryOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        craftingManager.TransferItemsToInventory();
    }

    private void TryOpenCampfireUI()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("CampfireBuilding"))
            {
                playerController.enabled = !playerController.enabled;
                OpenCampfireUI();
            }
        }
    }
    private void TryOpenCestUI()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("ChestBuilding"))
            {
                playerController.enabled = !playerController.enabled;
                OpenChestUI();
            }
        }
    }

    private void OpenCampfireUI()
    {
        campfireUI.SetActive(true);
        inventoryUI.SetActive(true);
        darkBackground.SetActive(true);
        crafting.SetActive(true);

        isCampfireOpen = true;
        isInventoryOpen = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void CloseCampfireUI()
    {
        campfireUI.SetActive(false);
        inventoryUI.SetActive(false);
        darkBackground.SetActive(false);
        crafting.SetActive(false);

        playerController.enabled = !playerController.enabled;
        isCampfireOpen = false;
        isInventoryOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OpenChestUI()
    {
        chestUI.SetActive(true);
        inventoryUI.SetActive(true);
        darkBackground.SetActive(true);
        crafting.SetActive(true);

        isChestOpen = true;
        isInventoryOpen = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    private void CloseChestUI()
    {
        chestUI.SetActive(false);
        inventoryUI.SetActive(false);
        darkBackground.SetActive(false);
        crafting.SetActive(false);

        playerController.enabled = !playerController.enabled;
        isChestOpen = false;
        isInventoryOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
