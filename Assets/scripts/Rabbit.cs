using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public int health = 3;
    public GameObject meatPrefab;
    private RabbitSpawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<RabbitSpawner>();
    }

    public void Kill()
    {
        health--;

        if (health <= 0)
        {
            Debug.Log("Rabbit has been killed!");
            SpawnMeat();
            Destroy(gameObject);
            spawner.SpawnRabbit();
        }
        else
        {
            Debug.Log("Rabbit hit! Remaining health: " + health);
        }
    }

    void SpawnMeat()
    {
        Vector3 spawnPosition = transform.position;
        Instantiate(meatPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Meat spawned at: " + spawnPosition);
    }
}
