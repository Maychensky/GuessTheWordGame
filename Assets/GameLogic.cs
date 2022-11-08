using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    [Tooltip("sprite for letter box")]
    private Sprite spriteForLetterBox;


    Word _word;
    Kayboard _kayboard;
    void Awake()
    {
        var test = new ButtonKay();
        var testWord = new Word();
    }
    void Test()
    {
         var testBox = new GameObject("box", typeof(SpriteRenderer));
         var comRender = testBox.GetComponent<SpriteRenderer>();
         comRender.sprite = spriteForLetterBox;
    }
}
