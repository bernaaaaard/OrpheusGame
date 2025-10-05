using System.Collections;
using UnityEngine;

public class CSlashAttack : MonoBehaviour
{
    [Header("Slash Settings")]
    public BoxCollider slashHitbox;     
    public int damage = 2;
    public float slashDuration = 0.5f;  
    public float cooldown = 2f;

    private bool isAttacking = false;
    private bool canAttack = true;

    private void Start()
    {
        if (slashHitbox != null)
            slashHitbox.enabled = false; 
    }

    public void PerformSlash()
    {
        if (!canAttack || isAttacking) return;

        //Debug.Log("Ceuthonymus performs a slash!");
        StartCoroutine(SlashRoutine());
    }

    private IEnumerator SlashRoutine()
    {
        canAttack = false;
        isAttacking = true;

       
        // animator.SetTrigger("Slash");

        yield return new WaitForSeconds(0.2f); 

       
        slashHitbox.enabled = true;

        yield return new WaitForSeconds(slashDuration);

        
        slashHitbox.enabled = false;

        isAttacking = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (slashHitbox == null) return;

        Gizmos.color = Color.red;
        Gizmos.matrix = slashHitbox.transform.localToWorldMatrix;
        Gizmos.DrawWireCube(slashHitbox.center, slashHitbox.size);
    }
}
