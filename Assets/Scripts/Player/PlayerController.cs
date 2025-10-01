using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]

    [SerializeField] float baseMovementSpeed = 5f;

    [Space(2)]

    [SerializeField] float baseMoveRotationSpeed = 360f;



    #region References

    CharacterController _characterController;

    #endregion



    #region Input Variables


    // References

    OrpheusControls _playerInputActions;

    // Private variables

    Vector3 _playerMovementInput;

    #endregion



    private void Awake()
    {
        _playerInputActions = new OrpheusControls();
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _playerInputActions.PlayerMap.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.PlayerMap.Disable();
    }


    private void Update()
    {
        GetPlayerMovementInput();
        ProcessLookDirection();
        ProcessPlayerMovement();
    }

    void GetPlayerMovementInput()
    {
        Vector2 movementInput = _playerInputActions.PlayerMap.Move.ReadValue<Vector2>();
        _playerMovementInput = new Vector3(movementInput.x, 0f, movementInput.y);

        Debug.Log(movementInput);
    }

    void ProcessPlayerMovement()
    { 
        Vector3 moveDirection = transform.forward * baseMovementSpeed * _playerMovementInput.magnitude * Time.deltaTime;
        _characterController.Move(moveDirection);
    }

    void ProcessLookDirection()
    {
        if (_playerMovementInput == Vector3.zero)
            return;

        Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 multipliedMatrix = isometricMatrix.MultiplyPoint3x4(_playerMovementInput);

        //Quaternion rotation = Quaternion.LookRotation(multipliedMatrix, Vector3.up);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, baseMoveRotationSpeed * Time.deltaTime);

        Vector3 relative = (transform.position + multipliedMatrix) - transform.position;
        Quaternion rot = Quaternion.LookRotation(relative, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, baseMoveRotationSpeed * Time.deltaTime);

    }
}
