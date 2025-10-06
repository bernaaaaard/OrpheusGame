using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    public int maxHealth = 10;
    public int currentHealth;

    private bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage! HP left: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        if (gameObject.name == "Ceuthonymus(Clone)")
        {
            SceneManager.LoadScene("MainMenu");
        }
        EnemyAI ai = GetComponent<EnemyAI>();
        if (ai != null)
        {
            ai.enabled = false;
        }
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject, 2f); 
    }
}
