using UnityEngine;

public class CampfireBuild : MonoBehaviour
{
    public GameObject replacementObject;
    [HideInInspector] public float interactionDistance = 4f;
    [HideInInspector] public int requiredStickCount = 6;
    [HideInInspector] public int requiredStoneCount = 4;
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
                if (hit.collider.gameObject.CompareTag("Campfire") && CanReplaceObject())
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
            int stickCount = inventoryManager.GetItemCount(ItemIdentificator.Stick);
            int stoneCount = inventoryManager.GetItemCount(ItemIdentificator.Stone);
            if (stickCount >= requiredStickCount && stoneCount >= requiredStoneCount)
            {
                return true;
            }
        }
        return false;
    }

    void ReplaceObject()
    {
        inventoryManager.RemoveItem(ItemIdentificator.Stick, requiredStickCount);
        inventoryManager.RemoveItem(ItemIdentificator.Stone, requiredStoneCount);

        Destroy(gameObject);
        Instantiate(replacementObject, transform.position, replacementObject.transform.rotation);
    }
}
