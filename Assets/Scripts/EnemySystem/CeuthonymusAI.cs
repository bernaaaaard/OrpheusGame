using System.Collections;
using UnityEngine;

public class CeuthonymusAI : EnemyAI
{
    [Header("Ceuthonymus Settings")]
    public float teleportCooldown = 5f;
    public float teleportDistanceFromPlayer = 3f;

    [Header("Attack Ranges")]
    public float slashRange = 2f;
    public float projectileRange = 8f;

    private float lastTeleportTime;

    private CSlashAttack slashAttack;
    private CProjectileAttack cprojectileAttack;

    private bool isTeleporting = false;

    protected override void Awake()
    {
        base.Awake();
        slashAttack = GetComponent<CSlashAttack>();
        cprojectileAttack = GetComponent<CProjectileAttack>();
    }

    protected override void UpdateChasingState()
    {
        if (playerTransform == null || isTeleporting) return;

        Vector3 lookPos = playerTransform.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        
        if (Time.time - lastTeleportTime >= teleportCooldown)
        {
            StartCoroutine(TeleportAndSlash());
            lastTeleportTime = Time.time;
            return;
        }

       
        if (distance <= slashRange)
        {
            slashAttack?.PerformSlash();
        }
       
        else if (distance <= projectileRange)
        {
            cprojectileAttack?.PerformAttack(playerTransform);
        }
    }

    private IEnumerator TeleportAndSlash()
    {
        isTeleporting = true;

       
        Debug.Log("Ceuthonymus preparing to teleport...");
        yield return new WaitForSeconds(0.5f);

        
        Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;
        Vector3 teleportPosition = playerTransform.position - dirToPlayer * teleportDistanceFromPlayer;

       
        teleportPosition.y = transform.position.y;
        transform.position = teleportPosition;

        Debug.Log("Ceuthonymus teleported near player!");

        
        slashAttack?.PerformSlash();

        yield return new WaitForSeconds(1f); 
        isTeleporting = false;
    }


    private void OnDrawGizmosSelected()
    {
       
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, slashRange);

        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, projectileRange);

        
        Gizmos.color = new Color(0.6f, 0f, 0.6f, 1f); 
        Gizmos.DrawWireSphere(transform.position, teleportDistanceFromPlayer);

        Gizmos.color = Color.yellow;
      
        Gizmos.DrawWireSphere(transform.position, sightRange);


       

    }
}
