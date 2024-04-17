using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // Reference na Slider pro zdrav� v editoru

    public float maxHealth = 100f; // Maxim�ln� hodnota zdrav�

    private float currentHealth; // Aktu�ln� hodnota zdrav�

    void Start()
    {
        currentHealth = maxHealth; // Nastav�me zdrav� na maxim�ln� hodnotu p�i startu
        DecreaseHealth(0);
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount; // Sn��me hodnotu zdrav� o ur�it� po�et
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ujist�me se, �e hodnota je v rozmez� 0 a maxHealth
        UpdateSlider(); // Zavol�me funkci pro aktualizaci hodnoty na Slideru
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount; // Zv���me hodnotu zdrav� o ur�it� po�et
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ujist�me se, �e hodnota je v rozmez� 0 a maxHealth
        UpdateSlider(); // Zavol�me funkci pro aktualizaci hodnoty na Slideru
    }

    void UpdateSlider()
    {
        healthSlider.value = currentHealth / maxHealth; // Nastav�me hodnotu Slideru zdrav� na z�klad� aktu�ln� hodnoty zdrav�
    }
}

