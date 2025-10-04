using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public int damage = 1;
    public float attackRange = 2.5f;
    private Transform playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    public void DealDamageToPlayer()
    {
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            //UnitHealth targetHealth = playerTransform.GetComponent<UnitHealth>();
            // if (targetHealth != null)
            // {
            //     targetHealth.DmgUnit(damage);
            // }
        }
    }
}
