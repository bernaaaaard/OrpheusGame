using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public UnitHealth _playerHealth = new UnitHealth(10, 10);

    public AugmentUIManager _augmentUIManager;

    public LamentationUIManager _lamentationUIManager;

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
        if (_lamentationUIManager)
        { 
            _lamentationUIManager.DisplayActiveLamentation();
        }

        
        
    }

    private void Update()
    {
        if (_augmentUIManager && _lamentationUIManager.LamentationActivated)
        {
            _augmentUIManager.ShowCards();
        }
    }
}
