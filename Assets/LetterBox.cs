using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

internal enum StateLetterBox
{
    Opened,
    Hidden,
}

internal class LetterBox 
{
    private const char DEFOLT_CONTENT = ' ';
    private const float DEFOLT_SIZE_CONTENT = 24f;
    private static GameObject _canvasObject;
    private static Sprite _defoltSprite;
    private GameObject _box;
    private GameObject _textForBox;
    private TextMeshProUGUI  _componentTMPForText;
    private Vector3 _position;
    internal Vector3 position
    {
        get {return _position; }
        set {_position = value; CorrectPosition(); }
    }

    private Vector2 _scaleBox;
    internal Vector2 scaleBox
    {
        get {return _scaleBox; }
        set {_scaleBox = value;  CorrectScaleBox(); }
    }
    private Sprite _spriteForBox;
    internal Sprite spriteForBox
    {
        get {return _spriteForBox; }
        set {_spriteForBox = value; }
    }

    private char _content;
    internal char content
    {
        get {return _content; }
        set {_content = content; CorrectContent(); }
    }

    private float _sizeContent;
    internal float sizeContent
    {
        get {return _sizeContent; }
        set {_sizeContent = value; CorrectSizeContent(); }
    }

    private Color _colorContent;
    internal Color colorContent
    {
        get {return colorContent; }
        set {_colorContent = value; CorrectColorContent(); }
    }

    internal Color colorBoxOpened { get; set; }
    internal Color colorBoxHidden { get; set; }

    static LetterBox()
    {
        AddCanvas();
        CreateDefoltSprite();
    }

    internal LetterBox () : this (Vector3.zero, Vector2.one, _defoltSprite, Color.white, Color.black, DEFOLT_CONTENT, DEFOLT_SIZE_CONTENT, Color.black) { }
    internal LetterBox(Vector3 position, Vector2 scaleBox, Sprite spriteForBox, Color colorBoxOpened, Color colorBoxHidden, char content, float sizeContent, Color colorContent)
    {
        _position = position;
        _scaleBox = scaleBox;
        _content = content;
        _sizeContent = sizeContent;
        _colorContent = colorContent;
        _spriteForBox = spriteForBox;
        this.colorBoxOpened = colorBoxOpened;
        this.colorBoxHidden = colorBoxHidden;
        CreateLetterBox(); 
    }

    private void CorrectPosition() => _box.transform.position = _position;
    private void CorrectScaleBox() => _box.transform.localScale.Set(_scaleBox.x, _scaleBox.y, _box.transform.localScale.z);
    private void CorrectContent() => _componentTMPForText.text = _content.ToString();
    private void CorrectSizeContent() => _componentTMPForText.fontSize = _sizeContent;
    private void CorrectColorContent() => _componentTMPForText.color = _colorContent;

    private static void AddCanvas()
    {
        _canvasObject = new GameObject("Canvas for boxes", typeof(Canvas) , typeof(RectTransform), typeof(GraphicRaycaster));
        _canvasObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
    }

    private static void CreateDefoltSprite()
    {
        _defoltSprite = Resources.Load<Sprite>("Square");
    }

    private void CreateLetterBox()
    {
        CreateBox();
        CreateContentForBox();
    }

    private void CreateBox()
    {
        _box = new GameObject("box", typeof(SpriteRenderer), typeof(RectTransform));
        _box.GetComponent<SpriteRenderer>().sprite = _spriteForBox;
        _box.transform.SetParent(_canvasObject.transform);  
        CreateParamsBox();
    }

    private void CreateParamsBox()
    {
        CorrectScaleBox();
        CorrectPosition();
    }

    private void CreateContentForBox()
    {
        _textForBox = new GameObject("TMP");
        _componentTMPForText = _textForBox.AddComponent<TextMeshProUGUI>();
        _textForBox.transform.SetParent(_box.transform);
        _componentTMPForText.alignment = TextAlignmentOptions.Center;
        CreateParamsTextForBox();
    }

    private void CreateParamsTextForBox()
    {
        CorrectContent();
        CorrectSizeContent();
        CorrectColorContent();
    }

    internal void SetActive(bool active) => _box.SetActive(active);
    internal void OpenedBox() => _box.GetComponent<SpriteRenderer>().color = colorBoxOpened;
    internal void HiddenBox() => _box.GetComponent<SpriteRenderer>().color = colorBoxHidden;
    internal void ChangeStateBox(StateLetterBox stateLetterBox) => throw new NotImplementedException();


    public override bool Equals(object other)
    {
        if (!(other is LetterBox)) return false;
        else return (Equals((LetterBox)other));
    }
    
    internal bool Equals(LetterBox other)
    {
        return _content == other._content;
    }



}