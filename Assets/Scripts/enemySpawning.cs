using UnityEngine;

public class enemySpawning : MonoBehaviour
{
    public GameObject fury;
    public GameObject shade;
    public GameObject ceuthynomus;
    public GameObject soul;

     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(fury, new Vector3(Random.Range(150, 190), 100, Random.Range(-1050, -1009)), Quaternion.identity);
        }
        Instantiate(soul, new Vector3(Random.Range(150, 190), 100, Random.Range(-1050, -1009)), Quaternion.identity);
        Instantiate(soul, new Vector3(Random.Range(15, 58), 100, Random.Range(-1050, -1009)), Quaternion.identity);
        Instantiate(soul, new Vector3(Random.Range(15, 58), 100, Random.Range(-1050, -1009)), Quaternion.identity);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(shade, new Vector3(Random.Range(15, 58), 100, Random.Range(-1050, -1009)), Quaternion.identity);
        }
        Instantiate(ceuthynomus, new Vector3(-111.25f, 101.13f, -1035.96f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
