using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatInteraction : MonoBehaviour
{
    public float interactDistance = 5f; // Distance within which the player can interact with the boat
    public InventoryManager inventoryManager; // Reference to the InventoryManager

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithBoat();
        }
    }

    void InteractWithBoat()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Boat"))
            {
                CheckInventoryForGas();
            }
        }
    }

    void CheckInventoryForGas()
    {
        if (inventoryManager.HasItemOfType(ItemIdentificator.Gas))
        {
            SceneManager.LoadScene("Escaped");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Debug.LogWarning("You need gas to escape with the boat!");
        }
    }
}
