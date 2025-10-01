using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuButtons : MonoBehaviour
{
    public void onClick() 
    {
        Debug.Log("hi");
        if (transform.name == "PlayButton") 
        {
            SceneManager.LoadScene("GameplayScene");
        }
    }
}
