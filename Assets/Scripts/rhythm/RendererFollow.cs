using UnityEngine;

public class RendererFollow : MonoBehaviour
{
    public Camera targetCamera;

    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    void LateUpdate()
    {
        // Make the object face the camera (billboard style)
        transform.rotation = Quaternion.LookRotation(
            transform.position - targetCamera.transform.position,
            targetCamera.transform.up
        );
    }
}