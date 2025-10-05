using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
    {
        Debug.Log("A projectile created");
        Destroy(gameObject, 5f);
    }

  
    void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.CompareTag("Player"))
        {
            
            if (collision.gameObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour player))
            {
                
                player.PlayerTakeDamage(1); 
            }
        }
        
        Destroy(gameObject);
    }
}