using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform firePoint; 
    public float projectileForce = 20f;
    public float attackCooldown = 3f;
    private float lastAttackTime;

    public void PerformAttack(GameObject target)
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            Debug.Log("Shade is firing");
            
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            
            Vector3 direction = (target.transform.position - firePoint.position).normalized;
            rb.AddForce(direction * projectileForce, ForceMode.Impulse);

            lastAttackTime = Time.time;
        }
    }
}