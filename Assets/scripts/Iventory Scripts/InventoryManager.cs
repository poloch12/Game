using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject InventoryItemPrefab;
    [HideInInspector] GameObject handItem;
    int selectedSlot = -1;
    /*public static InventoryManager instance;

    private void Awake()
    {
 
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of InventoryManager found. Destroying this instance.");
            Destroy(gameObject);
        }
    }*/
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

            Transform SpawnPoint = GameObject.Find("SpawnPoint").transform;
            PlaceItemInHand(recivedItem, SpawnPoint);

        }
        else
        {
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

    public void PlaceItemInHand(Item item, Transform spawnPoint)
    {
        if (HasItem(item))
        {

            if (handItem != null) 
            {
                Destroy(handItem);
                handItem = null;
            }
            // Vytvo�en� instance p�edm�tu (p�edm�t mus� m�t p�ednastaven� 3D model)
            GameObject newItem = Instantiate(item.prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
            handItem = newItem;
            var rigidbody = newItem.GetComponent<Rigidbody>();
            Destroy(rigidbody);

            // Um�st�n� nov�ho p�edm�tu na zvolen� m�sto
            newItem.transform.SetParent(spawnPoint); // Nastav� rodi�e nov�mu p�edm�tu na m�sto ur�en�
            newItem.transform.localPosition = Vector3.zero; // Nastav� lok�ln� pozici na nulu (aby se p�edm�t um�stil p�esn� na SpawnPoint)

            Debug.Log("P�edm�t " + item.name + " um�st�n do ruky na pozici: " + spawnPoint.position);
        }
        else
        {
            Debug.LogWarning("P�edm�t " + item.name + " nen� k dispozici v invent��i.");
        }
    }


}
