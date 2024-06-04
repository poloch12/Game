using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float raycastDistance = 5f; // Adjustable raycast distance

    void Update()
    {
        // Check if the "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            PerformRaycast();
        }
    }

    void PerformRaycast()
    {
        // Perform a raycast from the player's position forward
        Ray ray = new Ray(transform.position + transform.forward, transform.forward);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            Debug.Log("Raycast hit: " + hit.collider.name + " with tag: " + hit.collider.tag);

            // Check if the hit collider has the tag "cave entrance"
            if (hit.collider.CompareTag("caveEntrance"))
            {
                Debug.Log("Hit cave entrance, teleporting...");
                TeleportPlayerCave();
            }
            else if (hit.collider.CompareTag("CaveOut"))
            {
                TeleportPlayerToEntrance();
            }
            else
            {
                Debug.Log("Hit object is not tagged as cave entrance.");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    void TeleportPlayerCave()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject caveSpawn = GameObject.FindGameObjectWithTag("CaveSpawn");

        if (player != null && caveSpawn != null)
        {
            Debug.Log("Teleporting player to CaveSpawn position: " + caveSpawn.transform.position);

            CharacterController characterController = player.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
            }

            player.transform.position = caveSpawn.transform.position;

            if (characterController != null)
            {
                characterController.enabled = true;
            }

            Debug.Log("Player teleported to cave spawn.");
        }
        else
        {
            if (player == null)
            {
                Debug.LogWarning("Player with tag 'Player' not found.");
            }
            if (caveSpawn == null)
            {
                Debug.LogWarning("CaveSpawn with tag '" + caveSpawn + "' not found.");
            }
        }
    }

    void TeleportPlayerToEntrance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject CaveEntranceSpawn = GameObject.FindGameObjectWithTag("CaveEntranceSpawn");

        if (player != null && CaveEntranceSpawn != null)
        {
            Debug.Log("Teleporting player to CaveSpawn position: " + CaveEntranceSpawn.transform.position);

            CharacterController characterController = player.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
            }

            player.transform.position = CaveEntranceSpawn.transform.position;

            if (characterController != null)
            {
                characterController.enabled = true;
            }

            Debug.Log("Player teleported to cave spawn.");
        }
        else
        {
            if (player == null)
            {
                Debug.LogWarning("Player with tag 'Player' not found.");
            }
            if (CaveEntranceSpawn == null)
            {
                Debug.LogWarning("CaveSpawn with tag '" + CaveEntranceSpawn + "' not found.");
            }
        }
    }
}
