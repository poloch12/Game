using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

            if (spawnPoint != null)
            {
                CharacterController characterController = player.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    characterController.enabled = false;
                }

                player.transform.position = spawnPoint.transform.position;

                if (characterController != null)
                {
                    characterController.enabled = true;
                }
                player.transform.rotation = spawnPoint.transform.rotation;
            }
            else
            {
                Debug.LogError("No GameObject found with the 'SpawnPoint' tag!");
            }
        }
    }
}
