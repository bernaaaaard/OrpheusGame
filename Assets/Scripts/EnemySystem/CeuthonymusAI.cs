using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEditor.Rendering;

public class CeuthonymusAI : MonoBehaviour
{
    [Header("Teleport Settings")]
    public float teleportRange = 10f;
    public float teleportCooldown = 5f;
    public float minTeleportDistance = 3f;

     public float sightRange = 10f;
    public float attackRange = 3f;

    public float teleportHeight = 0.5f;

    private NavMeshAgent agent;
    private Transform playerTransform;
    private bool canTeleport = true;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {

        if (canTeleport && Vector3.Distance(transform.position, playerTransform.position) < minTeleportDistance)
        {
            StartCoroutine(TeleportRoutine());
        }
    }

    IEnumerator TeleportRoutine()
    {
        canTeleport = false;


        Vector3 randomDirection = Random.insideUnitSphere * teleportRange;
        randomDirection += transform.position;


        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, teleportRange, NavMesh.AllAreas))
        {

            yield return new WaitForSeconds(0.2f);

            Vector3 newPos = hit.position + Vector3.up ;
            transform.position = hit.position;


        }

        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
    }
    
    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.yellow;
      
        Gizmos.DrawWireSphere(transform.position, teleportRange);

       
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, minTeleportDistance);
    }
}
