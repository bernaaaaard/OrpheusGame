using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsScreen;
    private void Start()
    {
        mainMenu.SetActive(true);
        creditsScreen.SetActive(false);
    }

    public void onClick() 
    {
        if (transform.name == "PlayButton") 
        {
            SceneManager.LoadScene("Intro");
        }
        if (transform.name == "CreditsButton")
        {
            mainMenu.SetActive(false);
            creditsScreen.SetActive(true);
        }
        if (transform.name == "BackButton")
        {
            mainMenu.SetActive(true);
            creditsScreen.SetActive(false);
        }
    }
}
