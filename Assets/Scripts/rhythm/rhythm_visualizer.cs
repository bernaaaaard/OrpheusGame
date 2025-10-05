using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class rhythm_visualizer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(noteAnimation());
    }

    IEnumerator noteAnimation() 
    {
        for (int i = 0; i < 10; i++) 
        {
            gameObject.transform.position += new Vector3(0, 2, 0);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
        yield break;
    }
}
