using UnityEngine;

public class Billboarding : MonoBehaviour
{
    
    private void Update()
    {
        //transform.LookAt(Camera.main.transform.position, Vector3.up);



        transform.forward = Camera.main.transform.forward;


    }
}
