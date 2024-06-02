using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject replacementObject;
    [HideInInspector] public float interactionDistance = 2f;
    public LayerMask interactionLayer;
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
            int woodCount = inventoryManager.GetItemCount(ItemIdentificator.Wood);
            return woodCount >= requiredWoodCount;
        }
        return false;
    }

    void ReplaceObject()
    {
        inventoryManager.RemoveItem(ItemIdentificator.Wood, requiredWoodCount);

        Destroy(gameObject);
        Instantiate(replacementObject, transform.position, transform.rotation);
    }
}
