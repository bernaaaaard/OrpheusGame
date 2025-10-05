using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    private float lerpSpeed = 0.05f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager != null && GameManager.gameManager._playerHealth != null)
        {
            healthSlider.value = GameManager.gameManager._playerHealth.Health;

            if (healthSlider.value != easeHealthSlider.value)
            {
                easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, GameManager.gameManager._playerHealth.Health, lerpSpeed);
            }
        }

        

    }
}
