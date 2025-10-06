using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class introController : MonoBehaviour
{
    public VideoPlayer player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.Play();
        Debug.Log("ay");
    }

    private void Update()
    {
        if (!player.isPlaying) 
        {
            SceneManager.LoadScene("GameplayScene");
        }
    }
}
