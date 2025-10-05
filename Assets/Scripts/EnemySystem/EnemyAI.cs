using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Idle, Chasing, Attacking, Dead }
    protected EnemyState currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected NavMeshAgent navAgent;
    protected Transform playerTransform;
    //private UnitHealth health;
    private MeleeAttack meleeAttack;
    private ProjectileAttack projectileAttack;

    
    //Animation
    private Animator animator;

    // AI Settings 

    public float sightRange = 10f;
    public float attackRange = 3f;


    protected virtual void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        //health = GetComponent<UnitHealth>();

        meleeAttack = GetComponent<MeleeAttack>();
        projectileAttack = GetComponent<ProjectileAttack>();
        animator = GetComponent<Animator>();

    }



    protected virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = EnemyState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                UpdateIdleState();
                break;

            case EnemyState.Chasing:

                UpdateChasingState();
                break;

            case EnemyState.Attacking:
                UpdateAttackingState();
                break;

            case EnemyState.Dead:
                break;

        }
    }



    protected virtual void UpdateIdleState()
    {   if (navAgent)
        {
            navAgent.isStopped = true;
        }
       // Debug.Log("Code is working here");
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= sightRange)
        {
            currentState = EnemyState.Chasing;
        }
    }

    protected virtual void UpdateChasingState()
    {
        if (playerTransform == null) return;
        if (navAgent)
        {
            navAgent.isStopped = false;
            navAgent.SetDestination(playerTransform.position);
        }
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            currentState = EnemyState.Attacking;
        }
    }



    protected virtual void UpdateAttackingState()
    {
        if (navAgent)
        {
            navAgent.isStopped = true;
        }

        if (playerTransform == null)
            {
                currentState = EnemyState.Idle;
                return;
            }

        transform.LookAt(playerTransform);
        


        Debug.Log("Attacking!");
        //animator.SetTrigger("Attack");

        if (meleeAttack != null)
        {
            meleeAttack.PerformAttack();
        }
        else if (projectileAttack != null)
        {
            projectileAttack.PerformAttack(playerTransform.gameObject);
        }
        


        if (Vector3.Distance(transform.position, playerTransform.position) > attackRange)
        {
            currentState = EnemyState.Chasing;
        }

    }

     private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.yellow;
      
        Gizmos.DrawWireSphere(transform.position, sightRange);

       
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
