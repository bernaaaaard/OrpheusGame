using System.Collections;
using UnityEngine;

public class WhipAttack : MonoBehaviour
{
    [Header("Whip Settings")]
    public Vector3 boxSize = new Vector3(2f, 2f, 4f);  
    public int damage = 1;
    public float duration = 0.2f;
    public LayerMask playerLayer;

    private bool isAttacking = false;

    public void PerformWhip()
    {
        if (isAttacking) return;

        isAttacking = true;
        Debug.Log("Furys Whip attack!");
        StartCoroutine(WhipRoutine());
    }

    private IEnumerator WhipRoutine()
    {
        
        Vector3 boxCenter = transform.position + transform.forward * (boxSize.z / 2);

        Collider[] hits = Physics.OverlapBox(boxCenter, boxSize / 2, transform.rotation, playerLayer);

        foreach (var hit in hits)
        {
            Debug.Log("Hit by Fury Whip!");
            if (hit.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour player)) 
            {
                player.PlayerTakeDamage(1);
            }
        }

        yield return new WaitForSeconds(duration);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 boxCenter = transform.position + transform.forward * (boxSize.z / 2);
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}
