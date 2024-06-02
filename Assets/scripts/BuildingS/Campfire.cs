using UnityEngine;

public class Campfire : MonoBehaviour
{
    public GameObject replacementObject;
    [HideInInspector] public float interactionDistance = 2f;
    public LayerMask interactionLayer;
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
            if (Physics.Raycast(ray, out var hit, interactionDistance, interactionLayer))
            {

                if (Input.GetKeyDown(KeyCode.Z) && CanReplaceObject())
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
        Instantiate(replacementObject, transform.position, transform.rotation);
    }
}

