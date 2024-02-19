using UnityEngine;
using UnityEngine.AI;

public class HorrorAI : MonoBehaviour
{
    [Header("SCRIPT MADE BY GRAYSON DO NOT SHARE IT")]
    [SerializeField] private NavMeshAgent mob;

    public string targetTag; // Tag of the object to follow
    public float instanceY;
    public AudioSource roar;
    public float chaseRange = 4f; // Range for chasing
    public float wanderRange = 10f; // Range for wandering

    public float enemySpeed = 3.0f; // Adjust the enemy's speed here

    private Vector3 randomDestination; // Random destination for wandering

    private enum AIState { Wander, Chase }
    private AIState currentState = AIState.Wander;

    private void Start()
    {
        mob = GetComponent<NavMeshAgent>();
        mob.speed = enemySpeed; // Set the initial speed
    }

    private void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (targets.Length > 0)
        {
            Transform closestTarget = GetClosestTarget(targets);

            if (closestTarget != null)
            {
                float distanceToTarget = Vector3.Distance(transform.position, closestTarget.position);

                if (distanceToTarget <= chaseRange)
                {
                    // The target is within chase range, switch to chase mode
                    currentState = AIState.Chase;
                }
                else
                {
                    // The target is outside chase range, switch to wander mode
                    currentState = AIState.Wander;
                }

                if (currentState == AIState.Chase)
                {
                    mob.SetDestination(closestTarget.position);

                    // Check if the distance to the target is within chase range
                    if (distanceToTarget <= chaseRange)
                    {
                        // The target is within chase range, you can perform actions like roaring here
                    }
                }
            }
        }
        else
        {
            // No objects with the specified tag found, wander to a random point
            currentState = AIState.Wander;
        }

        if (currentState == AIState.Wander)
        {
            // Wander when in wander mode
            Wander();
        }
    }

    private Transform GetClosestTarget(GameObject[] targets)
    {
        Transform closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target.transform;
            }
        }

        return closestTarget;
    }

    private void Wander()
    {
        // Check if the AI has reached the random destination or has no destination
        if (!mob.hasPath || mob.remainingDistance <= 0.1f)
        {
            // Generate a new random destination within the wander range
            Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, wanderRange, 1);
            randomDestination = hit.position;

            // Set the AI's destination to the random point
            mob.SetDestination(randomDestination);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a gizmo to visualize the chase range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
