using UnityEngine;
using UnityEngine.AI;

public class RabbitController : MonoBehaviour
{
    public float patrolRange = 20f; // Range within which the rabbit will find new patrol points

    private NavMeshAgent agent;
    private Vector3 targetPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindRandomPoint(); // Find a random point to start with
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5f) // If rabbit reached destination
        {
            FindRandomPoint(); // Find a new random point
        }
    }

    void FindRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRange, 1);
        targetPoint = hit.position;
        agent.SetDestination(targetPoint);
    }
}
