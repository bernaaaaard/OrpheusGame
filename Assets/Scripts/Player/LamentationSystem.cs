using System.Collections.Generic;
using UnityEngine;

public class LamentationSystem : MonoBehaviour
{
    public static LamentationSystem instance;
    public List<LamentationSO> allLamentations = new List<LamentationSO>();

    // public properties

    public LamentationSO ActiveLamentation => activeLamentation;

    // private variables

    LamentationSO activeLamentation;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SelectRandomLamentation();
    }

    void SelectRandomLamentation()
    {
        if(allLamentations.Count < 1)
            return;
       
        int randomLamentationNo = Random.Range(0, allLamentations.Count);

        if (activeLamentation == allLamentations[randomLamentationNo])
        {
            SelectRandomLamentation();
        }

        else
        {
            activeLamentation = allLamentations[randomLamentationNo];
        }

         
    }

}
