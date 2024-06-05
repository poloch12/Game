using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject InventoryItemPrefab;
    [HideInInspector] GameObject handItem;
    int selectedSlot = -1;
    public InventoryOpener inventoryOpener;
    public GameObject StickInHand;
    public GameObject WoodInHand;
    public GameObject TorchInHand;
    public HungerBar hungerBar;
    //public static InventoryManager instance;

    private void Awake()
    {
        inventoryOpener = GameObject.Find("MainInventoryGroup").GetComponent<InventoryOpener>();
    }
    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);

            if (isNumber && number > 0 && number < 9)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
        // Check for use item action (for example, left mouse button click)
        if (Input.GetMouseButtonDown(0) && !inventoryOpener.isInventoryOpen) 
        { 
            UseItem();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            DropSelectedItem();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            InventoryItem selectedItem = inventorySlots[selectedSlot].GetComponentInChildren<InventoryItem>();
            if (selectedItem != null && selectedItem.item.stackable)
            {
                selectedItem.SplitStack();
            }
        }
    }

    void ChangeSelectedSlot(int newValue)
    { 
        
        if (selectedSlot >= 0)  
        { 
            inventorySlots[selectedSlot].DeSelect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;

        Item recivedItem = GetSelectedItem(false);
        if (recivedItem != null)
        {

            Transform SpawnPoint = GameObject.Find("ItemSpawnPoint").transform;
            PlaceItemInHand(recivedItem, SpawnPoint);

        }
        else
        {
            WoodInHand.SetActive(false);
            StickInHand.SetActive(false);
            TorchInHand.SetActive(false);
            Destroy(handItem);
            handItem = null;
        }
    }

    public bool AddItem(Item item)
    {
        //check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        //find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null ) 
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }
    public void AddSplitItem(Item item, int count)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                GameObject newItemGo = Instantiate(InventoryItemPrefab, slot.transform);
                InventoryItem newInventoryItem = newItemGo.GetComponent<InventoryItem>();
                newInventoryItem.InitialiseItem(item);
                newInventoryItem.count = count;
                newInventoryItem.RefreshCount();
                return;
            }
        }
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(InventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {

            //can use for creafting or building
            Item item = itemInSlot.item;
            if (use == true)
            {
                if (itemInSlot.item.stackable == true)
                {
                    itemInSlot.count--;
                    if (itemInSlot.count <= 0)
                    {
                        Destroy(itemInSlot.gameObject);
                    }
                    else
                    {
                        itemInSlot.RefreshCount();
                    }
                }
            }
            return item;
        }
        return null;
    }

    public bool HasItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item)
            {
                return true;
            }
        }
        return false;
    }
    public bool HasFreeSlot()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].GetComponentInChildren<InventoryItem>() == null)
            {
                return true;
            }
        }
        return false;
    }

    public void PlaceItemInHand(Item item, Transform spawnPoint)
    {
        if (HasItem(item))
        {
            StickInHand.SetActive(false);
            WoodInHand.SetActive(false);
            TorchInHand.SetActive(false);
            if (handItem != null) 
            {
                Destroy(handItem);
                handItem = null;
                TorchInHand.SetActive(false);
                StickInHand.SetActive(false);
                WoodInHand.SetActive(false);
            }
            if (item.identificator == ItemIdentificator.Stick)
            {
                StickInHand.SetActive(true);
            }
            else if (item.identificator == ItemIdentificator.Wood)
            {
                WoodInHand.SetActive(true);
            }
            else if (item.identificator == ItemIdentificator.Torch)
            {
                TorchInHand.SetActive(true);
            }
            else
            {
                TorchInHand.SetActive(false);
                StickInHand.SetActive(false);
                WoodInHand.SetActive(false);
                // Vytvoøení instance pøedmìtu (pøedmìt musí mít pøednastavený 3D model)
                GameObject newItem = Instantiate(item.prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
                handItem = newItem;
                var rigidbody = newItem.GetComponent<Rigidbody>();
                Destroy(rigidbody);

                // Umístìní nového pøedmìtu na zvolené místo
                newItem.transform.SetParent(spawnPoint); // Nastaví rodièe novému pøedmìtu na místo urèení
                newItem.transform.localPosition = Vector3.zero; // Nastaví lokální pozici na nulu (aby se pøedmìt umístil pøesnì na SpawnPoint)

                Debug.Log("Pøedmìt " + item.name + " umístìn do ruky na pozici: " + spawnPoint.position);
            }
        }
        else
        {
            Debug.LogWarning("Pøedmìt " + item.name + " není k dispozici v inventáøi.");
        }
    }

    void UseItem()
    {        
        Item selectedItem = GetSelectedItem(false);
        if (selectedItem == null || selectedItem.type == ItemType.Material && selectedItem.identificator != ItemIdentificator.CookedMeat)
        {
            return;
        }
        if (selectedItem.identificator == ItemIdentificator.CookedMeat)
        {
            hungerBar.IncreaseHunger(30f);
            GetSelectedItem(true);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (selectedItem.actionType == ActionType.Chop && hit.collider.CompareTag("Tree"))
            {
                hit.collider.GetComponent<Tree>().Chop();
            }
            else if (selectedItem.actionType == ActionType.Mine && hit.collider.CompareTag("Rock"))
            {
                hit.collider.GetComponent<Rock>().Mine();
            }
            else if (selectedItem.actionType == ActionType.Smash && hit.collider.CompareTag("Bush"))
            {
                hit.collider.GetComponent<Bush>().Smash();
            }
            else if (selectedItem.actionType == ActionType.Smash && hit.collider.CompareTag("Rabbit"))
            {
                hit.collider.GetComponent<Rabbit>().Kill();
            }
            else
            {
                Debug.LogWarning("Item cannot be used on this object.");
            }
        }
    }
    void DropSelectedItem()
    {
        Item selectedItem = GetSelectedItem(false);
        if (selectedItem == null)
        {
            Debug.LogWarning("No item selected to drop.");
            return;
        }

        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Vector3 dropPosition = Camera.main.transform.position + Camera.main.transform.forward * 2;
            GameObject droppedItem = Instantiate(selectedItem.prefabToSpawn, dropPosition, Quaternion.identity);
            droppedItem.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 2, ForceMode.Impulse);

            if (itemInSlot.item.stackable && itemInSlot.count > 1)
            {
                itemInSlot.count--;
                itemInSlot.RefreshCount();
            }
            else
            {
                Destroy(itemInSlot.gameObject);
                if (handItem != null)
                {
                    Destroy(handItem);

                    handItem = null;
                }
            }

            Debug.Log("Dropped item: " + selectedItem.name);
        }
    }
    public int GetItemCount(ItemIdentificator identificator)
    {
        int count = 0;
        foreach (var slot in inventorySlots)
        {
            var itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item.identificator == identificator)
            {
                count += itemInSlot.count;
            }
        }
        return count;
    }

    public void RemoveItem(ItemIdentificator identificator, int amount)
    {
        int remainingAmount = amount;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            var slot = inventorySlots[i];
            var itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item.identificator == identificator)
            {
                if (itemInSlot.count <= remainingAmount)
                {
                    remainingAmount -= itemInSlot.count;
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.count -= remainingAmount;
                    itemInSlot.RefreshCount();
                    break;
                }
            }
        }
    }
    public bool HasItemOfType(ItemIdentificator identificator)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventoryItem itemInSlot = inventorySlots[i].GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item.identificator == identificator)
            {
                return true;
            }
        }
        return false;
    }

}
