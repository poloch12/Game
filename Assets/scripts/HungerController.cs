using UnityEngine;

public class HungerController : MonoBehaviour
{
    public HungerBar hungerBar; // Reference na skript hunger baru

    public float starvationThreshold = 20f; // Hranice, kdy hr�� za�ne trp�t hladem

    void Update()
    {
        /*if (hungerBar.GetCurrentHunger() <= starvationThreshold)
        {
            // Prov�d�t akce, kter� indikuj� hlad hr��e
            Debug.Log("I'm hungry!");
        }*/
    }
}
