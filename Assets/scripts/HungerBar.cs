using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Slider hungerSlider; // Reference na Slider v editoru
    public Slider healthSlider; // Reference na Slider pro zdrav� v editoru

    public float maxHunger = 100f; // Maxim�ln� hodnota hunger baru
    public float hungerRate = 1f; // Rychlost, kterou se hunger sni�uje

    private float currentHunger; // Aktu�ln� hodnota hunger baru

    void Start()
    {
        currentHunger = maxHunger; // Nastav�me hunger na maxim�ln� hodnotu p�i startu
    }

    void Update()
    {
        DecreaseHunger(); // Zavol�me funkci pro sn�en� hunger baru
        UpdateSlider(); // Zavol�me funkci pro aktualizaci hodnoty na Slideru
    }

    void DecreaseHunger()
    {
        currentHunger -= hungerRate * Time.deltaTime; // Sn��me hodnotu hunger baru v �ase
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger); // Ujist�me se, �e hodnota je v rozmez� 0 a maxHunger
    }

    void UpdateSlider()
    {
        hungerSlider.value = currentHunger / maxHunger; // Nastav�me hodnotu Slideru na z�klad� aktu�ln� hodnoty hunger baru
    }

    public void IncreaseHunger(float amount)
    {
        currentHunger += amount; // Zv���me hodnotu hunger baru o ur�it� po�et
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger); // Ujist�me se, �e hodnota je v rozmez� 0 a maxHunger
    }

    public float GetCurrentHunger()
    {
        return currentHunger;
    }

    public void SetHealth(float healthPercentage)
    {
        healthSlider.value = healthPercentage; // Nastav�me hodnotu Slideru zdrav�
    }
}