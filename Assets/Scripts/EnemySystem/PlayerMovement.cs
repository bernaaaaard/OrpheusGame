using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 moveInput;


    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
     //   Debug.Log("Input Received: " + moveInput);
    }

    void Update()
    {
      
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(direction * speed * Time.deltaTime);
    }
}