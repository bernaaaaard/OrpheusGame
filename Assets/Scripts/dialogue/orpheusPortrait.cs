using UnityEngine;

public class orpheusPortrait : MonoBehaviour
{
    public dialogueController dialogueController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (Resources.Load<Texture2D>("Char_Orpheus") != null)
        {
            Texture2D portraitSprite = Resources.Load<Texture2D>("Char_Orpheus");
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(portraitSprite, new Rect(0, 0, portraitSprite.width, portraitSprite.height), Vector2.one * 0.5f);
            gameObject.transform.localScale = new Vector2(1, 2);
        }
        else
        {
            Debug.Log("cant find sprite image");
        }
    }

    private void Update()
    {
        if (dialogueController.dialogueArray[dialogueController.dialogueCount - 1].character.Trim() == "Orpheus")
        {
            Texture2D portraitSprite = Resources.Load<Texture2D>("Char_Orpheus");
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(portraitSprite, new Rect(0, 0, portraitSprite.width, portraitSprite.height), Vector2.one * 0.5f);
            gameObject.transform.localScale = new Vector2(1, 2);
        }
        else
        {
            Texture2D portraitSprite = Resources.Load<Texture2D>("Char_Orpheus_Shaded");
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(portraitSprite, new Rect(0, 0, portraitSprite.width, portraitSprite.height), Vector2.one * 0.5f);
            gameObject.transform.localScale = new Vector2(1, 2);
        }
    }
}
