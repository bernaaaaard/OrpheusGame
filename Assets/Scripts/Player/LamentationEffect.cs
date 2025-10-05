using UnityEngine;

[System.Serializable]
public abstract class LamentationEffect : ScriptableObject
{
    public abstract string EffectName();

    public virtual void ApplyEffect(GameObject playerObj)
    { 
    
    }
}
