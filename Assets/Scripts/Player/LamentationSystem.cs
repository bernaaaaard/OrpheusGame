using System.Collections.Generic;
using UnityEngine;

public class LamentationSystem : MonoBehaviour
{
    public static LamentationSystem instance;
    public List<LamentationSO> allLamentations = new List<LamentationSO>();
    public List<LamentationEffect> allLamentationEffects = new List<LamentationEffect>();

    // public properties

    public LamentationSO ActiveLamentation => activeLamentation;
    public LamentationEffect ActiveLamentationEffect => activeLamentationEffect;

    // private variables

    LamentationSO activeLamentation;
    LamentationEffect activeLamentationEffect;

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

        SelectRandomLamentation();
    }

    private void Start()
    {
        
    }

    public void SelectRandomLamentation()
    {
        if(allLamentations.Count < 1)
            return;

        //if (allLamentationEffects.Count < 1)
        //    return;
       


        int randomLamentationNo = Random.Range(0, allLamentations.Count);

        if (activeLamentation == allLamentations[randomLamentationNo])
        {
            SelectRandomLamentation();
        }

        //else if (allLamentations[randomLamentationNo].Title != allLamentationEffects[randomLamentationNo].EffectName())
        //{
        //    SelectRandomLamentation();

        //}

        else
        {
            activeLamentation = allLamentations[randomLamentationNo];
            //activeLamentationEffect = allLamentationEffects[randomLamentationNo];
        }

       
        
    }

}
