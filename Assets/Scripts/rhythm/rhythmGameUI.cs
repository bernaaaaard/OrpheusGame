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
    public bool qteInput = false;
    public bool coroutineRunning = false;
    public bool qteFlag = false;
    public int qteCount;
    public float timingFactor;
    public float zz;

    //public GameObject perfect;
    //public GameObject good;
    //public GameObject fail;
    void Start()
    {
        coroutineRunning = false;
        qteFinished = true;
        inputDetected = false;
        keyToPress = KeyCode.A;
        qteFlag = false;
        qteCount = 0;
        keyText.gameObject.SetActive(false);
        insideCircle.gameObject.SetActive(false);
        outsideCircle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        waitTime = rhythmController.bps / 21f;
        if (qteFinished && !coroutineRunning && qteFlag && qteCount < 3)
        {
            timingFactor = Random.Range(1, 3);
            switch (timingFactor)
            {
                case 1:
                    timingFactor = 1;
                    break;
                case 2:
                    timingFactor = 2;
                    break;
            }
            StartCoroutine(StartQTE(keyToPress));
            keyText.gameObject.SetActive(true);
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
                    //Instantiate(perfect);
                }
                else if (qtePercent <= 16 || qtePercent >= 24)
                {
                    Debug.Log("QTE Good");
                    //Instantiate(good);
                }
                else
                {
                    Debug.Log("QTE Fail");
                    //Instantiate(fail);
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
                qteCount++;
            }
            else
            {
                inputDetected = false;
            }
        }
        else if (qteCount == 3) 
        {
            keyText.gameObject.SetActive(false);
            insideCircle.gameObject.SetActive(false);
            outsideCircle.gameObject.SetActive(false);
            qteFlag = false;
            qteCount = 0;
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
            Vector3 currentPosition = new Vector3((x * 10) + 400, (y * 10) +200, zz);

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
        Debug.Log("idk bro");
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
            Vector3 currentPosition = new Vector3((x * 10) + 400, (y * 10) + 200, zz);

            insideCircle.SetPosition(i, currentPosition);
        }
    }
    IEnumerator StartQTE(KeyCode key)
    {
        insideCircle.gameObject.SetActive(true);
        outsideCircle.gameObject.SetActive(true);
        coroutineRunning = true;
        DrawInsideCircle(100, 1);
        for (float i = 3; i > 1; i -= 0.1f)
        {
            qtePercent = Mathf.Round(i * 10);
            DrawOutsideCircle(100, i);
            if (qteInput)
            {
                qteFinished = true;
                //QTEFinished();
                //yield break;
            }
            yield return new WaitForSeconds(waitTime * timingFactor);
        }
        if (qteInput) 
        {
            qteInput = false;
            qteFinished = false;
        }
            QTEFinished();
    }
}

