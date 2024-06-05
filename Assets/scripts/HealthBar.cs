using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // Reference to the Slider for health in the editor

    public float maxHealth = 100f; // Maximum health value

    private float currentHealth; // Current health value

    void Start()
    {
        currentHealth = maxHealth; // Set health to maximum value at start
        DecreaseHealth(0);
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount; // Decrease the health value by a certain amount
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure the value is within 0 and maxHealth
        UpdateSlider(); // Call the function to update the slider value
        CheckHealth(); // Check the health to see if we need to load the "Death" scene
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount; // Increase the health value by a certain amount
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure the value is within 0 and maxHealth
        UpdateSlider(); // Call the function to update the slider value
    }

    void UpdateSlider()
    {
        healthSlider.value = currentHealth / maxHealth; // Set the slider value based on the current health value
    }

    void CheckHealth()
    {
        if (healthSlider.value <= 0.01f) // If the slider value is 0 or less (considering precision issues)
        {
            SceneManager.LoadScene("Death"); // Load the "Death" scene
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}