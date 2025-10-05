using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public int damage = 1;
    public float attackDamageRange = 2.5f;
     public float attackCooldown = 2f;
    private float lastAttackTime;

    void Start()
    {
        
    }

  
    public void PerformAttack()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {

            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {

                if (Vector3.Distance(transform.position, playerObject.transform.position) <= attackDamageRange)
                {

                    if (playerObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour player))
                    {

                        player.PlayerTakeDamage(damage);
                    }
                }
            }
            lastAttackTime = Time.time;
        }
    }
}
