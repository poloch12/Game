using UnityEngine;

public class HungerController : MonoBehaviour
{
    public HungerBar hungerBar; // Reference na skript hunger baru

    public float starvationThreshold = 20f; // Hranice, kdy hráè zaène trpìt hladem

    void Update()
    {
        /*if (hungerBar.GetCurrentHunger() <= starvationThreshold)
        {
            // Provádìt akce, které indikují hlad hráèe
            Debug.Log("I'm hungry!");
        }*/
    }
}
