using System.Collections;
using UnityEngine;

public class CProjectileAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Projectile Settings")]
    public float cooldown = 3f;
    public float projectileSpeed = 10f;
    public float spreadAngle = 15f;

    private bool isOnCooldown = false;

    public void PerformAttack(Transform player)
    {
        if (projectilePrefab == null || player == null || isOnCooldown) return;

        StartCoroutine(ProjectileRoutine(player));
    }

    private IEnumerator ProjectileRoutine(Transform player)
    {
        isOnCooldown = true;

        Vector3 dirToPlayer = (player.position - transform.position).normalized;

        
        FireProjectile(dirToPlayer); 
        FireProjectile(Quaternion.Euler(0, -spreadAngle, 0) * dirToPlayer); 
        FireProjectile(Quaternion.Euler(0, spreadAngle, 0) * dirToPlayer);  

        Debug.Log("Ceuthonymus fires projectile!");

        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }

    private void FireProjectile(Vector3 direction)
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

        if (proj.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {

            rb.linearVelocity = direction * projectileSpeed;
        }

        //Destroy(proj, 5f);
    }
}
