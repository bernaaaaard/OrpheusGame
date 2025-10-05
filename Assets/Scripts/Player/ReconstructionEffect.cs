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

    public override void ApplyEffect(MonoBehaviour playerObj)
    {
        EnemyDetection(playerObj);
    }

    void SetupRangeDetector(MonoBehaviour playerObj)
    {
        _sphereCollider = new SphereCollider();
        _sphereCollider.center = playerObj.transform.position;
        _sphereCollider.radius = enemyDetectedRange;
        _sphereCollider.isTrigger = true;

    }

    void EnemyDetection(MonoBehaviour playerObj)
    {
        playerObj.StartCoroutine(EnemyDetectionRoutine(playerObj));
        playerObj.StartCoroutine(HealingTimerRoutine(playerObj));
    }

    IEnumerator EnemyDetectionRoutine(MonoBehaviour playerObj)
    {
        while (true)
        {
            // Old method, will keep incase its needed

            //if (Physics.SphereCast(playerObj.transform.position, enemyDetectedRange, playerObj.transform.forward, out RaycastHit hitInfo))
            //{
            //    if (hitInfo.collider.TryGetComponent<EnemyAI>(out EnemyAI enemy))
            //    {
            //        Debug.Log("Enemy detected!");

            //        _isEnemyNear = true;
            //        outOfCombatTimer = 0.0f;
            //        healingTimer = healingTimerLength;
            //    }

            //    else
            //    {
            //        Debug.Log("Out of range of enemies!");
            //        _isEnemyNear = false;
            //    }
            //}


            Collider[] numOfColliders = Physics.OverlapSphere(playerObj.transform.position, enemyDetectedRange);

            int numOfEnemies = 0;

            foreach (Collider collider in numOfColliders)
            {

                if (collider.TryGetComponent<EnemyAI>(out EnemyAI enemy))
                {
                    numOfEnemies += 1;

                    Debug.Log("Enemy detected!");

                    _isEnemyNear = true;
                    outOfCombatTimer = 0.0f;
                    healingTimer = healingTimerLength;
                }

                else if(numOfColliders.Length < 1 || numOfEnemies < 1)
                {
                    Debug.Log("Out of range of enemies!");
                    _isEnemyNear = false;
                }
            }

            yield return null;
        }
    }

    IEnumerator HealingTimerRoutine(MonoBehaviour playerObj)
    {
        while (true)
        {
            if (_isEnemyNear == false)
            {
                Debug.Log("Out of range timer started");
                outOfCombatTimer += Time.deltaTime;

                if (outOfCombatTimer >= outOfCombatTimerLength)
                {
                    if (healingTimer >= healingTimerLength && GameManager.gameManager._playerHealth.Health < GameManager.gameManager._playerHealth.MaxHealth)
                    {
                        Debug.Log("Healed 1 point!");
                        playerObj.GetComponent<PlayerBehaviour>().PlayerHeal(1);
                        healingTimer = 0.0f;
                    }

                    else
                    {
                        healingTimer += Time.deltaTime;
                    }
                    
                }
            }

            yield return null;
        }

        
    }
}
