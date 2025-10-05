using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private OrpheusControls _controls;
    public GameObject deathMenu;

    int timesTookDamage;
    int timesTookDamageMax;
    int damageToTakeAmount;

    bool isDenialActive = false;

    private void Start()
    {
        if (GameManager.gameManager != null)
            GameManager.gameManager._playerHealth.OnDeath += PlayerDie;
        else
            Debug.LogError("GameManager is missing");

        timesTookDamage = 0;
    }


    private void Awake()
    {
        _controls = new OrpheusControls();

        _controls.PlayerMap.Damage.performed += ctx => PlayerTakeDamage(10);
        //_controls.PlayerMap.Heal.performed += ctx => PlayerHeal(10);
        Debug.Log("error in this line of heal code, add back in later");
    }

    private void OnEnable()
    {
        _controls.PlayerMap.Enable();
    }

    private void OnDisable()
    {
        _controls.PlayerMap.Disable();
    }

    public void PlayerTakeDamage(int dmg)
    {
        damageToTakeAmount = dmg;
        timesTookDamage += 1;

        CalculateAttackMiss();

        GameManager.gameManager._playerHealth.DmgUnit(damageToTakeAmount);
        if (GameManager.gameManager._playerHealth.Health > 0)
        {
            Debug.Log("Player took damage! Current HP: " + GameManager.gameManager._playerHealth.Health);
        }
    }

    public void PlayerHeal(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
        Debug.Log("Player healed! Current HP: " + GameManager.gameManager._playerHealth.Health);
    }

    public void PlayerDie()
    {
        Debug.Log("💀 Player has died!");

        GetComponent<PlayerController>().enabled = false;
        _controls.Disable();
        deathMenu.SetActive(true);
    }

    public void CalculateAttackMiss(int attacksBeforeMiss = 0)
    {
        if (attacksBeforeMiss == 0)
            return;

        if (attacksBeforeMiss > 0)
        {
            timesTookDamageMax = attacksBeforeMiss;

            isDenialActive = true;

            
        }
    }

    void CalculateAttackMiss()
    {
        if (isDenialActive)
        {
            if (timesTookDamage > timesTookDamageMax)
            {
                damageToTakeAmount = 0;
                Debug.Log("Attack dodged!");
                timesTookDamage = 0;
            }
        }
    }
    
}
