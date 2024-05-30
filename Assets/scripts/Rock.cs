using UnityEngine;

public class Rock : MonoBehaviour
{
    public int health = 3;
    public GameObject stonePrefab;

    public void Mine()
    {
        health--;

        if (health <= 0)
        {
            Debug.Log("Rock has been mined!");
            SpawnResource();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Rock hit! Remaining health: " + health);
        }
    }

    void SpawnResource()
    {
        Vector3 spawnPosition = transform.position;
        Instantiate(stonePrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Stone resource spawned at: " + spawnPosition + transform.rotation);
    }
}
