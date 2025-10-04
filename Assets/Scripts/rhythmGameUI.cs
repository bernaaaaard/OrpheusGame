using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class rhythmGameUI : MonoBehaviour
{
    public LineRenderer outsideCircle;
    public LineRenderer insideCircle;
    public bool qteFinished;
    public bool inputDetected;
    public rhythmController rhythmController;
    public float waitTime;
    private float qtePercent;
    private KeyCode keyToPress;
    public TMP_Text keyText;
    private bool qteInput = false;
    public bool coroutineRunning = false;
    void Start()
    {
        coroutineRunning = false;
        qteFinished = true;
        inputDetected = false;
        keyToPress = KeyCode.A;
    }

    // Update is called once per frame
    void Update()
    {
        waitTime = rhythmController.bps / 21f;
        if (qteFinished && !coroutineRunning)
        {
            StartCoroutine(StartQTE(keyToPress));
            qteFinished = false;
        }
        else if (!qteFinished) 
        {
                if (UnityEngine.Input.GetKeyDown(keyToPress))
                {
                    inputDetected = true;
                    qteInput = true;    
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
                    int random = Random.Range(1, 4);
                    switch (random) 
                    {
                        case 1:
                            keyToPress = KeyCode.A;
                            keyText.text = "A";
                            break;
                        case 2:
                            keyToPress = KeyCode.D;
                            keyText.text = "D";
                            break;
                        case 3:
                            keyToPress = KeyCode.W;
                            keyText.text = "W";
                            break;
                    }
                } else 
                {
                    inputDetected = false;
                }
        }
    }

    void DrawOutsideCircle(int steps, float radius) 
    {
        outsideCircle.positionCount = steps + 1;

        for (int i = 0; i <= steps; i++) 
        {
            float circumferenceProgress = (float)i / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3 (x, y, 0);

            outsideCircle.SetPosition(i, currentPosition);
        }
    }

    void QTEFinished() 
    {
        StartCoroutine(SetQTEFinishedNextFrame());
        coroutineRunning = false;
    }

    IEnumerator SetQTEFinishedNextFrame()
    {
        yield return null; // wait 1 frame
        qteFinished = true;
    }

    void DrawInsideCircle(int steps, float radius)
    {
        insideCircle.positionCount = steps + 1;

        for (int i = 0; i <= steps; i++)
        {
            float circumferenceProgress = (float)i / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x, y, 0);

            insideCircle.SetPosition(i, currentPosition);
        }
    }
    IEnumerator StartQTE(KeyCode key)
    {
        coroutineRunning = true;
        DrawInsideCircle(100, 1);
        for (float i = 3; i > 1; i -= 0.1f)
        {
            qtePercent = Mathf.Round(i * 10);
            DrawOutsideCircle(100, i);
            if (qteInput)
            {
                qteInput = false;
                QTEFinished();
                yield break;
            }
            yield return new WaitForSeconds(waitTime);
        }
        if (!qteFinished)
        {
            QTEFinished();
        }
    }
}

