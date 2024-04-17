using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // Reference na Slider pro zdraví v editoru

    public float maxHealth = 100f; // Maximální hodnota zdraví

    private float currentHealth; // Aktuální hodnota zdraví

    void Start()
    {
        currentHealth = maxHealth; // Nastavíme zdraví na maximální hodnotu pøi startu
        DecreaseHealth(0);
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount; // Snížíme hodnotu zdraví o urèitý poèet
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ujistíme se, že hodnota je v rozmezí 0 a maxHealth
        UpdateSlider(); // Zavoláme funkci pro aktualizaci hodnoty na Slideru
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount; // Zvýšíme hodnotu zdraví o urèitý poèet
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ujistíme se, že hodnota je v rozmezí 0 a maxHealth
        UpdateSlider(); // Zavoláme funkci pro aktualizaci hodnoty na Slideru
    }

    void UpdateSlider()
    {
        healthSlider.value = currentHealth / maxHealth; // Nastavíme hodnotu Slideru zdraví na základì aktuální hodnoty zdraví
    }
}

