using UnityEngine;

public class CSlashHitbox : MonoBehaviour
{
    public int damage = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Gets Slashed");
            if (other.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour player))
            {
                player.PlayerTakeDamage(damage);
                Debug.Log($"Player hit by SlashAttack for {damage} damage!");
            }
        }
    }
}
