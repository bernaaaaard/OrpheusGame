using UnityEngine;

[CreateAssetMenu(fileName = "AngerEffect", menuName = "Lamentation/AngerEffect")]
public class AngerEffect : LamentationEffect
{
    public int Multiplier = 2;

    public override string EffectName()
    {
        return "Anger";
    }

    public override void ApplyEffect(GameObject playerObj)
    {
        playerObj?.GetComponent<PlayerController>().CalculateDamageToGive(Multiplier);
    }
}
