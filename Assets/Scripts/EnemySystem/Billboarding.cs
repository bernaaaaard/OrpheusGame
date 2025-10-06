using UnityEngine;

public class DirectionalBillboarding : MonoBehaviour
{
    public Transform visualsTransform;
    public SpriteRenderer spriteRenderer;

    public Sprite frontSprite;
    public Sprite backSprite;

    private Camera mainCamera;
    private Vector3 lastPosition;

    void Start()
    {
        mainCamera = Camera.main;
        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        
        if (mainCamera != null)
        {
            visualsTransform.rotation = mainCamera.transform.rotation;
        }

        
        Vector3 currentVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        
        
        if (currentVelocity.sqrMagnitude < 0.1f)
        {
            spriteRenderer.sprite = frontSprite;
            spriteRenderer.flipX = false;
            return;
        }

        
        Vector3 moveDirection = currentVelocity.normalized;
        moveDirection.y = 0;
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;
        
        
        float dotProduct = Vector3.Dot(moveDirection, cameraForward);
        if (dotProduct > 0)
        {
            spriteRenderer.sprite = backSprite;
        }
        else
        {
            spriteRenderer.sprite = frontSprite;
        }

       
        Vector3 crossProduct = Vector3.Cross(cameraForward, moveDirection);
        if (crossProduct.y > 0)
        {
            
            spriteRenderer.flipX = false; 
        }
        else
        {
           
            spriteRenderer.flipX = true;
        }
    }
}