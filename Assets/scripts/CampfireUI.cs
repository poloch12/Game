using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampfireUI : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public InventorySlot meatSlot;
    public InventorySlot fuelSlot;
    public InventorySlot resultSlot;
    public Slider fuelSlider;
    public Button addFuelButton;
    public Button cookButton;
    public Item cookedMeatItem;

    private float fuel = 0f;
    private float maxFuel = 100f;
    private float fuelConsumptionRate = 1f;
    private float fuelToCook = 10f;

    private void Start()
    {
        fuelSlider.maxValue = maxFuel;
        UpdateFuelUI();

        addFuelButton.onClick.AddListener(AddFuel);
        cookButton.onClick.AddListener(CookMeat);
    }

    private void Update()
    {
        if (fuel > 0)
        {
            fuel -= fuelConsumptionRate * Time.deltaTime;
            if (fuel < 0)
            {
                fuel = 0;
            }
            UpdateFuelUI();
        }
    }

    private void AddFuel()
    {
        InventoryItem fuelItem = fuelSlot.GetComponentInChildren<InventoryItem>();
        if (fuelItem != null)
        {
            if (fuelItem.item.identificator == ItemIdentificator.Wood)
            {
                fuel += 50f;
                fuelItem.count--;
            }
            else if (fuelItem.item.identificator == ItemIdentificator.Stick)
            {
                fuel += 10f;
                fuelItem.count--;
            }

            if (fuelItem.count <= 0)
            {
                Destroy(fuelItem.gameObject);
            }
            else
            {
                fuelItem.RefreshCount();
            }

            if (fuel > maxFuel)
            {
                fuel = maxFuel;
            }

            UpdateFuelUI();
        }
    }

    private void CookMeat()
    {
        if (fuel >= fuelToCook)
        {
            InventoryItem meatItem = meatSlot.GetComponentInChildren<InventoryItem>();
            if (meatItem != null && meatItem.item.identificator == ItemIdentificator.Meat)
            {
                fuel -= fuelToCook;

                InventoryItem cookedMeatItem = resultSlot.GetComponentInChildren<InventoryItem>();
                if (cookedMeatItem != null && cookedMeatItem.item.identificator == ItemIdentificator.CookedMeat)
                {
                    cookedMeatItem.count++;
                    cookedMeatItem.RefreshCount();
                }
                else
                {
                    SpawnCookedMeat();
                }

                meatItem.count--;
                if (meatItem.count <= 0)
                {
                    Destroy(meatItem.gameObject);
                }
                else
                {
                    meatItem.RefreshCount();
                }

                UpdateFuelUI();
            }
        }
    }

    private void UpdateFuelUI()
    {
        fuelSlider.value = fuel;
    }

    private void SpawnCookedMeat()
    {
        GameObject newItemGO = Instantiate(inventoryManager.InventoryItemPrefab, resultSlot.transform);
        InventoryItem newItem = newItemGO.GetComponent<InventoryItem>();
        newItem.InitialiseItem(cookedMeatItem);
        newItem.count = 1;
        newItem.RefreshCount();
    }
}
