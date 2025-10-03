using UnityEngine;

public class orpheusPortrait : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Resources.Load<Texture2D>("Fury") != null)
        {
            Texture2D portraitSprite = Resources.Load<Texture2D>("Fury");
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(portraitSprite, new Rect(0, 0, portraitSprite.width, portraitSprite.height), Vector2.one * 0.5f);
            gameObject.transform.localScale = new Vector2(2, 2);
        }
        else
        {
            Debug.Log("cant find sprite image");
        }
    }
}
