using UnityEngine;

public class Tree : MonoBehaviour
{
    public int health = 3;
    public GameObject woodPrefab;
    public float spawnHeightOffset = 1f; // Height offset for spawning items

    public void Chop()
    {
        health--;

        if (health <= 0)
        {
            Debug.Log("Tree has been chopped down!");
            SpawnResource();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Tree hit! Remaining health: " + health);
        }
    }

    void SpawnResource()
    {
        Vector3 spawnPosition = transform.position + Vector3.up * spawnHeightOffset;
        Instantiate(woodPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Wood resource spawned at: " + spawnPosition);
    }
}
