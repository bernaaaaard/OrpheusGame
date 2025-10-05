using UnityEngine;
using UnityEngine.SceneManagement;

public class gameplaySceneButtons : MonoBehaviour
{
    public void onClick() 
    {
        if (transform.name == "ReturnToMenuButton") 
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (transform.name == "ResumeButton")
        {
            gameStateController.paused = false;
            Debug.Log("no longer paused");
        }
        if (transform.name == "RespawnButton") 
        {
            //input respawn scene
        }
    }
}
