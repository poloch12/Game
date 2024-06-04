using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Slider hungerSlider; // Reference na Slider v editoru
    public Slider healthSlider; // Reference na Slider pro zdraví v editoru

    public float maxHunger = 100f; // Maximální hodnota hunger baru
    public float hungerRate = 1f; // Rychlost, kterou se hunger snižuje

    private float currentHunger; // Aktuální hodnota hunger baru

    void Start()
    {
        currentHunger = maxHunger; // Nastavíme hunger na maximální hodnotu pøi startu
    }

    void Update()
    {
        DecreaseHunger(); // Zavoláme funkci pro snížení hunger baru
        UpdateSlider(); // Zavoláme funkci pro aktualizaci hodnoty na Slideru
    }

    void DecreaseHunger()
    {
        currentHunger -= hungerRate * Time.deltaTime; // Snížíme hodnotu hunger baru v èase
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger); // Ujistíme se, že hodnota je v rozmezí 0 a maxHunger
    }

    void UpdateSlider()
    {
        hungerSlider.value = currentHunger / maxHunger; // Nastavíme hodnotu Slideru na základì aktuální hodnoty hunger baru
    }

    public void IncreaseHunger(float amount)
    {
        currentHunger += amount; // Zvýšíme hodnotu hunger baru o urèitý poèet
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger); // Ujistíme se, že hodnota je v rozmezí 0 a maxHunger
    }

    public float GetCurrentHunger()
    {
        return currentHunger;
    }

    public void SetHealth(float healthPercentage)
    {
        healthSlider.value = healthPercentage; // Nastavíme hodnotu Slideru zdraví
    }
}