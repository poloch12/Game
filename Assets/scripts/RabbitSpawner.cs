using UnityEngine;

public class RabbitSpawner : MonoBehaviour
{
    public GameObject rabbitPrefab; // Assign the rabbit prefab in the Inspector
    private GameObject currentRabbit;

    void Start()
    {
        SpawnRabbit();
    }

    public void SpawnRabbit()
    {
        //if (currentRabbit == null)
        //{
            Vector3 spawnPosition = transform.position;
            currentRabbit = Instantiate(rabbitPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Rabbit spawned at: " + spawnPosition);
        //}
    }
}