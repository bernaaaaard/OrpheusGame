using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class rhythmGameUI : MonoBehaviour
{
    public LineRenderer circleRenderer;
    public bool qteFinished;
    public bool inputDetected;
    public rhythmController rhythmController;
    public float waitTime;
    private float qtePercent;
    private KeyCode keyToPress;
    public TMP_Text keyText;
    void Start()
    {
        qteFinished = true;
        inputDetected = false;
        keyToPress = KeyCode.A;
    }

    // Update is called once per frame
    void Update()
    {
        waitTime = rhythmController.bps / 21f;
        if (qteFinished)
        {
            StartCoroutine(StartQTE(keyToPress));
            qteFinished = false;
        }
        else if (!qteFinished) 
        {
            if (gameObject.transform.name == "Outside Circle")
            {
                if (UnityEngine.Input.GetKeyDown(keyToPress))
                {
                    inputDetected = true;
                    qteFinished = true;
                    Debug.Log(qtePercent);
                    if (qtePercent <= 13 || qtePercent >= 27)
                    {
                        Debug.Log("QTE Perfect");
                    }
                    else if (qtePercent <= 16 || qtePercent >= 24)
                    {
                        Debug.Log("QTE Good");
                    }
                    else 
                    {
                        Debug.Log("QTE Fail");
                    }
                    int random = Random.Range(1, 3);
                    if (random == 1)
                    {
                        keyToPress = KeyCode.A;
                        keyText.text = "A";
                    }
                    else 
                    {
                        keyToPress = KeyCode.D;
                        keyText.text = "D";
                    }
                } else 
                {
                    inputDetected = false;
                }
            }
        }
    }

    void DrawCircle(int steps, float radius) 
    {
        circleRenderer.positionCount = steps + 1;

        for (int i = 0; i <= steps; i++) 
        {
            float circumferenceProgress = (float)i / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3 (x, y, 0);

            circleRenderer.SetPosition(i, currentPosition);
        }
    }
    IEnumerator StartQTE(KeyCode key) 
    {
        if (!inputDetected)
        {
            if (gameObject.transform.name == "Outside Circle")
            {
                for (float i = 3; i > 1; i -= 0.1f)
                {
                    qtePercent = Mathf.Round(i * 10);
                    DrawCircle(100, i);
                    if (inputDetected)
                    { 
                        qteFinished = true;
                    }
                    yield return new WaitForSeconds(waitTime);
                }
                qteFinished = true;
            }
            else
            {
                DrawCircle(100, 1);
            }
        }
    }
}

