using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager != null && GameManager.gameManager._playerHealth != null)
        {
            healthSlider.value = GameManager.gameManager._playerHealth.Health;
        }

    }
}
