using UnityEngine;

[CreateAssetMenu(fileName = "DenialEffect", menuName = "Lamentation/DenialEffect")]
public class DenialEffect : LamentationSO
{
    public int numOfAttacksBeforeMiss = 2;

    int currentNumOfAttacks = 0;

    public override void ApplyEffect(MonoBehaviour playerObj)
    {
        playerObj.GetComponent<PlayerBehaviour>().CalculateAttackMiss(numOfAttacksBeforeMiss);
    }
}
