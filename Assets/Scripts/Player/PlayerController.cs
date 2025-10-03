using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    #region References

    [Header("References")]

    [SerializeField] GameObject playerModelPivot;

    CharacterController _characterController;

    #endregion

    [Space(5)]

    #region Movement

    [Header("Movement Settings")]

    [SerializeField] float maxMovementSpeed = 5f;
    [SerializeField] float accelerationFactor = 5f;
    [SerializeField] float decelerationFactor = 10f;

    [Space(2)]

    [SerializeField] float baseMoveRotationSpeed = 360f;

    // private variables

    float _currentSpeed;

    #endregion

    [Space(5)]

    #region Aiming

    [Header("Aiming Settings")]

    [SerializeField] GameObject pointerObj;
    [SerializeField] Transform aimingSpawnPosition;

    [Space(2)]

    [SerializeField] GameObject objectPosOne;
    [SerializeField] GameObject objectPosTwo;
    [SerializeField] GameObject objectPosThree;


    // private variables

    Vector3 _collidedTargetPoint;
    Vector3 _screenPos;
    Vector3 _worldPos;
    Vector3 _requiredHitPoint;

    LayerMask _playerLayer;

    #endregion


   


    #region Input Variables


    // References

    OrpheusControls _playerInputActions;

    // Private variables

    Vector3 _playerMovementInput;
    Vector3 _aimingInput;

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
        GetPlayerAimingInput();
        ProcessLookDirection();
        CalculateSpeed();
        ProcessMovement();
        ProcessAiming();
    }

    void GetPlayerMovementInput()
    {
        Vector2 movementInput = _playerInputActions.PlayerMap.Move.ReadValue<Vector2>();
        _playerMovementInput = new Vector3(movementInput.x, 0f, movementInput.y);

        Debug.Log(movementInput);
    }

    void CalculateSpeed()
    {
        if (_playerMovementInput == Vector3.zero && _currentSpeed > 0f)
        {
            _currentSpeed -= decelerationFactor * Time.deltaTime;
        }

        else if (_playerMovementInput != Vector3.zero && _currentSpeed < maxMovementSpeed)
        {
            _currentSpeed += accelerationFactor * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, maxMovementSpeed);
    }

    void ProcessMovement()
    { 
        Vector3 moveDirection = transform.forward * _currentSpeed * _playerMovementInput.magnitude * Time.deltaTime;
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

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, baseMoveRotationSpeed * Time.deltaTime);


        
        playerModelPivot.transform.position = _characterController.transform.position;
        transform.rotation = rot;
        playerModelPivot.transform.rotation = Quaternion.RotateTowards(playerModelPivot.transform.rotation, rot, baseMoveRotationSpeed * Time.deltaTime);

    }

    void GetPlayerAimingInput()
    {
        Vector2 aimingInput = _playerInputActions.PlayerMap.Aim.ReadValue<Vector2>();
        _aimingInput = new Vector3(aimingInput.x, 0.0f, aimingInput.y);

        Debug.Log("Mouse / Aim Pos: " + aimingInput);
    }

    void ProcessAiming()
    {
        //Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        //Ray cameraRay = Camera.main.ScreenPointToRay(_aimingInput);

        //float hitDist = 0.0f;

        //if (groundPlane.Raycast(cameraRay, out hitDist))
        //{ 
        //    Vector3 targetPoint = cameraRay.GetPoint(hitDist);
        //    _collidedTargetPoint = targetPoint;

        //    Debug.DrawLine(cameraRay.origin, targetPoint, Color.red);

        //    _screenPos = new Vector3(_aimingInput.x, _aimingInput.y, Camera.main.farClipPlane);

        //    _worldPos = Camera.main.ScreenToWorldPoint(_screenPos);

        //    Vector3 finalAimingPoint = new Vector3(targetPoint.x, targetPoint.y, targetPoint.z);

        //    pointerObj.transform.position = finalAimingPoint;
        //}


        // --------- DIFFERENT ATTEMPT TO SOLVE ISSUE ---------------

        Vector2 aimingInput = _playerInputActions.PlayerMap.Aim.ReadValue<Vector2>();
        Vector3 mousePos = Mouse.current.position.ReadValue();

        Ray cameraRay = Camera.main.ScreenPointToRay(mousePos);

        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit, Mathf.Infinity, -_playerLayer))
        {
            // Length of the triangle

            Vector3 playerHeight = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
            Debug.Log("Player Height Pos: " + playerHeight);

            Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            Debug.Log("Hit Point Pos: " + hitPoint);

            float length = Vector3.Distance(playerHeight, hitPoint);

            // Length of the hypotenuse

            var deg = 30;

            var rad = deg * Mathf.Deg2Rad;

            float hypotenuse = length / (Mathf.Sin(rad));

            float distanceFromCamera = hit.distance;

            // Changes based on player height

            if (this.transform.position.y > hit.point.y)
            {
                _requiredHitPoint = cameraRay.GetPoint(distanceFromCamera - hypotenuse);
            }

            else if (this.transform.position.y < hit.point.y)
            {
                _requiredHitPoint = cameraRay.GetPoint(distanceFromCamera + hypotenuse);
            }

            else
            {
                _requiredHitPoint = cameraRay.GetPoint(distanceFromCamera);
            }

            objectPosOne.transform.position = hitPoint;
            objectPosTwo.transform.position = playerHeight;
            objectPosThree.transform.position = _requiredHitPoint;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_requiredHitPoint, 3.0f);
    }
}
