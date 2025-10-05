using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public UnitHealth _playerHealth = new UnitHealth(10, 10);

    public AugmentUIManager _augmentUIManager;

    private void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }

        
    }

    private void Start()
    {
        if (_augmentUIManager)
        {
            _augmentUIManager.ShowCards();
        }
        
    }
}
