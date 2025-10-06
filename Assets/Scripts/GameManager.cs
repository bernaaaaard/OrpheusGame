using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public UnitHealth _playerHealth = new UnitHealth(3, 3);

    public AugmentUIManager _augmentUIManager;

    public LamentationUIManager _lamentationUIManager;

    public DialogueQueues dialogue;

    private void Awake()
    {
        _playerHealth = new UnitHealth(3, 3);
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
        if (_lamentationUIManager != null)
        {
            _lamentationUIManager.SetActiveLamentation();
            _lamentationUIManager.DisplayActiveLamentation();
        }

        
        
    }

    private void Update()
    {
        if (_augmentUIManager && _lamentationUIManager.LamentationActivated)
        {
            //_augmentUIManager.ShowCards();
        }
    }
}
