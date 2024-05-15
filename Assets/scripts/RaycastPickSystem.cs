using UnityEngine;

public class RaycastPickup : MonoBehaviour
{
    public float pickupRange = 5f;
    public LayerMask pickupLayer;

    private Camera cam;
    private InventoryManager inventoryManager;

    private void Start()
    {
        cam = Camera.main;
        inventoryManager = GetComponent<InventoryManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
            {
                InventoryItem item = hit.collider.GetComponentInChildren<InventoryItem>();
                if (item != null)
                {
                    // Add the item to the inventory
                    bool result = inventoryManager.AddItem(item.item);

                    if (result)
                    {
                        Debug.Log("Item picked up: " + item.item.name);
                        // Optionally, you can destroy the game object after picking up the item
                        Destroy(hit.collider.gameObject);
                    }
                    else
                    {
                        Debug.Log("Inventory is full. Cannot pick up item: " + item.item.name);
                    }
                }
            }
        }
    }
}

