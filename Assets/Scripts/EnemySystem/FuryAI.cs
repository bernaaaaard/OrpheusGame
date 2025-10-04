using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FuryAI : EnemyAI
{
    [Header("Fury Settings")]
    public float dashSpeed = 12f;
    public float dashDistance = 5f;
    public float dashWindupTime = 0.5f; 
    public float dashCooldown = 2f;
    public float dashDamage = 1f;

    public float chaseSpeed = 3f;

    private bool isDashing = false;
    private bool canDash = true;
    private Vector3 dashDirection;
    private Vector3 dashStartPosition;

    private WhipAttack whipAttack;

    protected override void Awake()
    {
        base.Awake();
        whipAttack = GetComponent<WhipAttack>();


    }

    protected override void UpdateChasingState()
    {

          if (!canDash || isDashing || playerTransform == null) return;

        transform.LookAt(playerTransform);
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if ( canDash && !isDashing)
        {
            currentState = EnemyState.Attacking;
            StartCoroutine(DashRoutine());
        }

       
        
    }

     protected override void UpdateAttackingState()
    {
        // if ( playerTransform == null) return;

        // float distance = Vector3.Distance(transform.position, playerTransform.position);
        // if (distance > attackRange)
        // {
        //     currentState = EnemyState.Chasing;
        // }

        // if (isDashing || !canDash) return;


        // StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        canDash = false;
        isDashing = true;


        if (playerTransform != null)
            transform.LookAt(playerTransform);


        yield return new WaitForSeconds(dashWindupTime);


        dashStartPosition = transform.position;
        dashDirection = (playerTransform.position - transform.position).normalized;

        while (Vector3.Distance(dashStartPosition, transform.position) < dashDistance)
        {
            float player_distance = Vector3.Distance(transform.position, playerTransform.position);
            if (player_distance <= attackRange) break;
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            yield return null;
        }


        isDashing = false;


        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange)
        {
            Debug.Log("Fury performs Whip!");
            whipAttack.PerformWhip();
        }


        yield return new WaitForSeconds(dashCooldown);


        canDash = true;
        currentState = EnemyState.Chasing;

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!isDashing) return;

        if (collider.CompareTag("Player"))
        {
            Debug.Log("Fury dashes dealing " + dashDamage + " dmg!");
        }   
    }

}
