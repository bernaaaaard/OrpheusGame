using UnityEngine;

[CreateAssetMenu(fileName = "New Lamentation", menuName = "Lamentation/Effect")]
public class LamentationSO : ScriptableObject
{
    public string Title;
    [TextArea(1,2)]
    public string Description;
    public Sprite Image;
}
