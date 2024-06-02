using UnityEngine;

public class ChestBuild : MonoBehaviour
{
    public GameObject replacementObject;
    [HideInInspector] public float interactionDistance = 4f;
    [HideInInspector] public int requiredWoodCount = 8;
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
            {
                if (hit.collider.gameObject.CompareTag("Chest") && CanReplaceObject())
                {
                    ReplaceObject();
                }
            }
        }
    }

    bool CanReplaceObject()
    {
        if (inventoryManager != null)
        {
            int woodCount = inventoryManager.GetItemCount(ItemIdentificator.Wood);
            return woodCount >= requiredWoodCount;
        }
        return false;
    }

    void ReplaceObject()
    {
        inventoryManager.RemoveItem(ItemIdentificator.Wood, requiredWoodCount);

        Destroy(gameObject);
        Instantiate(replacementObject, transform.position, replacementObject.transform.rotation);
    }
}
