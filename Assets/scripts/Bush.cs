using UnityEngine;

public class Bush : MonoBehaviour
{
    public int health = 3;
    public GameObject stickPrefab;
    public float spawnHeightOffset = 1f;

    public void Smash()
    {
        health--;

        if (health <= 0)
        {
            Debug.Log("Bush has been mined!");
            SpawnResource();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Bush hit! Remaining health: " + health);
        }
    }

    void SpawnResource()
    {
        Vector3 spawnPosition = transform.position + Vector3.up * spawnHeightOffset;
        Instantiate(stickPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Wood resource spawned at: " + spawnPosition);
    }
}
