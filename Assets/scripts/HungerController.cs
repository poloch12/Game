using UnityEngine;

public class HungerController : MonoBehaviour
{
    private HungerBar hungerBar; // Reference na skript hunger baru

    public float starvationThreshold = 0f; // Hranice, kdy hráè zaène trpìt hladem

    private HealthBar healthBar;



    private void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        hungerBar = GameObject.Find("HungerBar").GetComponent<HungerBar>();

        InvokeRepeating("Starvation",0.1f,1f);
    }
  
    private void Starvation()
    {
        if (hungerBar.GetCurrentHunger() <= starvationThreshold)
            {
                // Provádìt akce, které indikují hlad hráè
                healthBar.DecreaseHealth(2);
            }
    }
}
