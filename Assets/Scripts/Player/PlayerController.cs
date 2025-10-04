using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

    [Space(2)]

    [SerializeField] float maxGravitySpeed = -10.0f;

    // private variables

    float _currentSpeed;

    Vector3 _velocity;

    #endregion

    [Space(5)]

    #region Aiming

    [Header("Aiming References")]

    [SerializeField] GameObject pointerObj;
    [SerializeField] Transform aimingSpawnPosition;
    [SerializeField] LayerMask groundMask;

    [Space(2)]

    [Header("Aiming Debug References / Options")]

    [SerializeField] GameObject objectPosOne;
    [SerializeField] GameObject objectPosTwo;
    [SerializeField] GameObject objectPosThree;

    [Space(2)]

    [SerializeField] bool showDebugPosition = true;
    [SerializeField] bool ignoreHeight = true;
    


    // private variables

    Vector3 _collidedTargetPoint;
    Vector3 _screenPos;
    Vector3 _worldPos;
    Vector3 _requiredHitPoint;

    LayerMask _playerLayer;

    #endregion

    [Space(5)]

    #region Dashing

    [Header("Dashing Settings")]

    [SerializeField] float maxDashSpeed = 20.0f;
    [Range(0.1f, 1.0f),SerializeField] float dashLength = 0.5f;
    [SerializeField] float dashCooldownTimer = 2.0f;

    // private variables

    bool _isDashing = false;
    bool _canDash = true;

    #endregion

    [Space(5)]

    #region Firing

    [Header("Firing References")]

    [SerializeField] Transform bulletSpawnPosition;

    [Space(2)]

    [Header("Firing Settings")]

    [SerializeField] int bulletDamage = 1;
    [SerializeField] float fireRate = 0.2f;

    // private variables

    bool _isFiring = false;
    bool _canFire = false;

    Vector3 _firingDirection;

    #endregion



    #region Input Variables


    // References

    OrpheusControls _playerInputActions;

    // Private variables

    Vector3 _playerMovementInput;
    Vector3 _aimingInput;
    bool _dashInput;
    bool _fireInput;

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

    private void Start()
    {
        _canDash = true;
    }

    private void Update()
    {
        // Gravity

        ProcessGravity();

        // Input Functions

        GetPlayerMovementInput();
        GetPlayerAimingInput();
        GetDashingInput();
        GetFiringInput();

        // Movement / Aiming
        ProcessLookDirection();
        CalculateSpeed();
        ProcessMovement();
        ProcessAiming();
        HandleAiming();

        // Firing

        ProcessFiring();

        // Dashing

        ProcessDash();
    }

    #region Movement Functions

    void GetPlayerMovementInput()
    {
        Vector2 movementInput = _playerInputActions.PlayerMap.Move.ReadValue<Vector2>();
        _playerMovementInput = new Vector3(movementInput.x, 0f, movementInput.y);

        //Debug.Log(movementInput);
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
        if (_isDashing)
        {
            _characterController.Move(transform.forward * maxDashSpeed * _playerMovementInput.magnitude * Time.deltaTime);

            return;
        }

        Vector3 moveDirection = transform.forward * _currentSpeed * _playerMovementInput.magnitude * Time.deltaTime + _velocity;
        _characterController.Move(moveDirection);
    }

    void ProcessLookDirection()
    {


        if (_playerMovementInput == Vector3.zero)
        {
            playerModelPivot.transform.position = _characterController.transform.position;
            return;
        }
            
        Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 multipliedMatrix = isometricMatrix.MultiplyPoint3x4(_playerMovementInput);

        //Quaternion rotation = Quaternion.LookRotation(multipliedMatrix, Vector3.up);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, baseMoveRotationSpeed * Time.deltaTime);

        Vector3 relative = (transform.position + multipliedMatrix) - transform.position;
        Quaternion rot = Quaternion.LookRotation(relative, Vector3.up);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, baseMoveRotationSpeed * Time.deltaTime);



        playerModelPivot.transform.position = _characterController.transform.position;
        transform.rotation = rot;
        //playerModelPivot.transform.rotation = Quaternion.RotateTowards(playerModelPivot.transform.rotation, rot, baseMoveRotationSpeed * Time.deltaTime);

    }

    // Created function incase it is required at some point

    Vector3 HandleGravity()
    {
        bool isGrounded = _characterController.isGrounded;

        Vector3 gravityVec = new Vector3(0.0f, maxGravitySpeed, 0.0f);
        return gravityVec;
    }

    void ProcessGravity()
    {
        bool isGrounded = _characterController.isGrounded;

        //Debug.Log("Is player grounded? - " + isGrounded);

        if (isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }

        else if (!isGrounded)
        {
            _velocity.y = maxGravitySpeed * Time.deltaTime;
        }
    }

    #endregion

    #region Aiming Functions

    void GetPlayerAimingInput()
    {
        Vector2 aimingInput = _playerInputActions.PlayerMap.Aim.ReadValue<Vector2>();
        _aimingInput = aimingInput;

        //Debug.Log("Mouse / Aim Pos: " + aimingInput);
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

        if (Physics.Raycast(cameraRay, out hit, Mathf.Infinity))
        {
            // Length of the triangle

            Vector3 playerHeight = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
           // Debug.Log("Player Height Pos: " + playerHeight);

            Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
           // Debug.Log("Hit Point Pos: " + hitPoint);

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

            if (showDebugPosition)
            {
                objectPosOne.SetActive(true);
                objectPosTwo.SetActive(true);
                objectPosThree.SetActive(true);

                objectPosOne.transform.position = hitPoint;
                objectPosTwo.transform.position = playerHeight;
                objectPosThree.transform.position = _requiredHitPoint;
            }

            else
            {
                objectPosOne.SetActive(false);
                objectPosTwo.SetActive(false);
                objectPosThree.SetActive(false);
            }


        }
    }

    

    (bool success, Vector3 position) GetMousePosition()
    {
        Vector2 aimingInput = _playerInputActions.PlayerMap.Aim.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(aimingInput);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, groundMask))
        {
            Debug.DrawLine(ray.origin, hitInfo.point);
            return (success: true, position: hitInfo.point);
        }

        else
        {
            Debug.DrawLine(ray.origin, hitInfo.point);
            return (success: false, position : Vector3.zero);
        }

        
    } // UNUSED METHOD - WAS TESTING DIFFERENT WAYS TO ACHIEVE THE SAME TASK

    void MouseAim()
    { 
        var (success, position) = GetMousePosition();
        if (success)
        {
            Vector3 direction = position - transform.position;

            direction.y = 0f;

            playerModelPivot.transform.forward = direction;
        }
    } // SAME AS ABOVE - HAVE A SIMILAR VERSION BELOW THAT IS USED

    void HandleAiming()
    {
        Vector3 direction = _requiredHitPoint - transform.position;
        _firingDirection = direction;

        if (ignoreHeight)
        {
            direction.y = 0f;
        }

        playerModelPivot.transform.forward = direction;
    }

    #endregion

    #region Firing Functions

    void GetFiringInput()
    {
        bool fireInput = _playerInputActions.PlayerMap.Attack.IsPressed();
        _fireInput = fireInput;

        //Debug.Log("Player is firing: " + _fireInput);
    }

    void ProcessFiring()
    {
        Ray firingRay = new Ray(bulletSpawnPosition.transform.position, bulletSpawnPosition.transform.forward);

        RaycastHit fireHit;

        Debug.DrawRay(firingRay.origin, firingRay.direction * 100f, Color.darkRed);

        if (_isFiring && Physics.Raycast(firingRay, out fireHit, 150f))
        {
            if (fireHit.collider)
            {
                Debug.Log("I have hit something");
            }

            Debug.Log(fireHit.transform.gameObject.name);
        }
    }

    IEnumerator FiringRoutine()
    {
        _canFire = false;


        
        yield return new WaitForSeconds(fireRate);
        _canFire = true;
    }

    #endregion

    #region Dash Functions

    void GetDashingInput()
    {
        bool dashInput = _playerInputActions.PlayerMap.Dash.IsPressed();
        _dashInput = dashInput;

       // Debug.Log("Player has dashed: " + dashInput);
    }

    void ProcessDash()
    {
        if (_dashInput && _canDash && _currentSpeed > 0f)
        {
            StartCoroutine(DashRoutine());
        }
    }

    IEnumerator DashRoutine()
    { 
        _canDash = false;
        _isDashing = true;
        yield return new WaitForSeconds(dashLength);
        _isDashing = false;
        yield return new WaitForSeconds(dashCooldownTimer);
        _canDash = true;
        
    }

    #endregion
}
