using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ReconstructionEffect", menuName = "Lamentation/ReconstructionEffect")]
public class ReconstructionEffect : LamentationSO
{
    [Header("Timer Settings")]

    [SerializeField] float outOfCombatTimerLength = 10f;
    [SerializeField] float healingTimerLength = 8f;

    [Space(2)]

    [Header("Range Settings")]

    [SerializeField] float enemyDetectedRange = 10f;

    [Space(2)]

    [Header("Coroutine Settings")]

    [SerializeField] float timeBetweenRoutineRuns = 0.2f;

    // private variables

    SphereCollider _sphereCollider;

    bool _isEnemyNear = false;

    float outOfCombatTimer = 0.0f;
    float healingTimer = 0.0f;

    public override void ApplyEffect(GameObject playerObj)
    {
        
    }

    void SetupRangeDetector(GameObject playerObj)
    {
        _sphereCollider = new SphereCollider();
        _sphereCollider.center = playerObj.transform.position;
        _sphereCollider.radius = enemyDetectedRange;
        _sphereCollider.isTrigger = true;

    }

    void EnemyDetection(GameObject playerObj)
    {
        if (Physics.SphereCast(playerObj.transform.position, enemyDetectedRange, playerObj.transform.forward, out RaycastHit hitInfo))
        { 
            
        }
    }

    IEnumerator EnemyDetectionRoutine(GameObject playerObj)
    {
        while (true)
        {
            if (Physics.SphereCast(playerObj.transform.position, enemyDetectedRange, playerObj.transform.forward, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.TryGetComponent<EnemyAI>(out EnemyAI enemy))
                {
                    _isEnemyNear = true;
                    outOfCombatTimer = 0.0f;
                    healingTimer = healingTimerLength;
                }

                else
                { 
                    
                }
            }
        }
    }

    IEnumerator HealingTimerRoutine(GameObject playerObj)
    {
        while (true)
        {
            if (_isEnemyNear == false)
            {
                outOfCombatTimer += Time.deltaTime;

                if (outOfCombatTimer >= outOfCombatTimerLength)
                {
                    if (healingTimer >= healingTimerLength && GameManager.gameManager._playerHealth.Health < GameManager.gameManager._playerHealth.MaxHealth)
                    {
                        playerObj.GetComponent<PlayerBehaviour>().PlayerHeal(1);
                        healingTimer = 0.0f;
                    }

                    else
                    {
                        healingTimer += Time.deltaTime;
                    }
                    
                }
            }
        }
    }
}
