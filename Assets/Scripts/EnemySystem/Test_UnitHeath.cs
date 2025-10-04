using UnityEngine;

public class Test_UnitHealth : MonoBehaviour
{
    public int maxHealth = 10; 
    
    public void DmgUnit(int damage)
    {
        Debug.Log(gameObject.name + " took " + damage + " damage!");
    }
}