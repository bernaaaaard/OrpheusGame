using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Progression/Card")]
public class Card : ScriptableObject
{
    public new string name;
    [TextArea(3, 5)]
    public string description;
    public Sprite icon;
    
}
