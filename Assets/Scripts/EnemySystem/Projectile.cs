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
        Debug.Log("Projectile hit something named: " + collision.gameObject.name);
        
        if (collision.gameObject.TryGetComponent<Test_UnitHealth>(out Test_UnitHealth health))
        {
            health.DmgUnit(1);
        }

        if (collision.gameObject.TryGetComponent(out PlayerBehaviour playerHealth))
        {
            playerHealth.PlayerTakeDamage(1);
        }

        
        Destroy(gameObject);
    }
}