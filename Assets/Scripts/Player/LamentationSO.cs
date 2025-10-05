using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "New Lamentation", menuName = "Lamentation/Effect")]
public abstract class LamentationSO : ScriptableObject
{
    public string Title;
    [TextArea(1,2)]
    public string Description;
    public Sprite Image;

    public virtual void ApplyEffect(MonoBehaviour playerObj)
    {

    }
}
