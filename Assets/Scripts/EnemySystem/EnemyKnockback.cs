using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyKnockback : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Rigidbody rb;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    
    public void ApplyKnockback(Vector3 direction, float force)
    {
        StartCoroutine(KnockbackRoutine(direction, force));
    }

    private IEnumerator KnockbackRoutine(Vector3 direction, float force)
    {
        
        navAgent.enabled = false;
        rb.isKinematic = false;

        
        rb.AddForce(direction * force, ForceMode.Impulse);

        
        yield return new WaitForSeconds(0.5f);

        
        rb.isKinematic = true;
        navAgent.enabled = true;
    }
}