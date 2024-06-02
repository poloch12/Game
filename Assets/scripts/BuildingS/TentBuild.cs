using UnityEngine;

public class TentBuild : MonoBehaviour
{
    public GameObject replacementObject;
    [HideInInspector] public float interactionDistance = 4f;
    [HideInInspector] public int requiredWoodCount = 2;
    [HideInInspector] public int requiredStickCount = 10;
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
                if (hit.collider.gameObject.CompareTag("Tent") && CanReplaceObject())
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
            int stickCount = inventoryManager.GetItemCount(ItemIdentificator.Stick);
            int stoneCount = inventoryManager.GetItemCount(ItemIdentificator.Stone);
            if (woodCount >= requiredWoodCount && stickCount >= requiredStickCount && stoneCount >= requiredStoneCount)
            {
                return true;
            }
        }
        return false;
    }

    void ReplaceObject()
    {
        inventoryManager.RemoveItem(ItemIdentificator.Wood, requiredWoodCount);
        inventoryManager.RemoveItem(ItemIdentificator.Stick, requiredStickCount);
        inventoryManager.RemoveItem(ItemIdentificator.Stone, requiredStoneCount);

        Destroy(gameObject);
        Instantiate(replacementObject, replacementObject.transform.position, replacementObject.transform.rotation);
    }
}
