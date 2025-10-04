using UnityEngine;
using UnityEngine.UI;

public class npcPortraits : MonoBehaviour
{
    public void setPortrait(string portrait) 
    {
        if (Resources.Load<Texture2D>(portrait) != null)
        {
            Texture2D portraitSprite = Resources.Load<Texture2D>(portrait);
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(portraitSprite, new Rect(0, 0, portraitSprite.width, portraitSprite.height), Vector2.one * 0.5f);
            gameObject.transform.localScale = new Vector2(2, 2);
        }
        else 
        {

        }
    }
}
