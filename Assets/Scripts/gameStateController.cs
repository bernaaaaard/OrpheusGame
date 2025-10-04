using UnityEngine;

public class gameStateController : MonoBehaviour
{
    public static bool paused;
    public Canvas canvas;
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            paused = true;
            Debug.Log("paused");
        }
        if (paused)
        {
            canvas.enabled = true;
        }
        else 
        {
            canvas.enabled=false;
        }
    }
}
