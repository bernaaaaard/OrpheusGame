using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private OrpheusControls _controls;

    private void Start()
    {
        if (GameManager.gameManager != null)
            GameManager.gameManager._playerHealth.OnDeath += PlayerDie;
        else
            Debug.LogError("GameManager is missing");
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

    private void PlayerTakeDamage(int dmg)
    {
        GameManager.gameManager._playerHealth.DmgUnit(dmg);
        if (GameManager.gameManager._playerHealth.Health > 0)
        {
            Debug.Log("Player took damage! Current HP: " + GameManager.gameManager._playerHealth.Health);
        }
    }

    private void PlayerHeal(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
        Debug.Log("Player healed! Current HP: " + GameManager.gameManager._playerHealth.Health);
    }

    private void PlayerDie()
    {
        Debug.Log("💀 Player has died!");

        GetComponent<PlayerController>().enabled = false;
        _controls.Disable();
    }

}
