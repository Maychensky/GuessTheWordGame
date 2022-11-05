using UnityEngine;
using UnityEngine.UI;
using TMPro;

internal class ButtonKay
{
    private const char DEFOLT_CONTENT = ' ';
    private const float DEFOLT_SIZE_CONTENT = 24f;
    private static GameObject _canvasObject;
    private GameObject _button;
    private GameObject _textForButton;
    private TextMeshProUGUI _componentTMPForText;
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

    private Color _colorBox;
    internal Color colorBox
    {
        get {return _colorBox; }
        set {_colorBox = value; CorrectColorBox(); }
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

    static ButtonKay()
    {
        AddCanvas();
    }

    internal ButtonKay() : this( new(), new(1, 1), Color.white, DEFOLT_CONTENT, DEFOLT_SIZE_CONTENT, Color.black) { }
    internal ButtonKay(char content): this (new(), new(1, 1), Color.white, content, DEFOLT_SIZE_CONTENT, Color.black) { }
    internal ButtonKay(Vector3 position, Color colorBox, char content, Color colorContent) : this( position, new(1, 1), colorBox, content, DEFOLT_SIZE_CONTENT, colorContent) { }
    internal ButtonKay( Vector3 position, Vector2 scaleBox, Color colorBox, char content, float sizeContent, Color colorContent)
    {
        _position = position;
        _scaleBox = scaleBox;
        _colorBox = colorBox;
        _content = content;
        _sizeContent = sizeContent;
        _colorContent = colorContent;
        CreateButtonKey();
    }

    private void CorrectPosition() => _button.transform.position = _position;
    private void CorrectScaleBox() => _button.transform.localScale.Set(_scaleBox.x, _scaleBox.y, _button.transform.localScale.z);
    private void CorrectColorBox() => _button.GetComponent<Image>().color = _colorBox;
    private void CorrectContent() => _componentTMPForText.text = _content.ToString();
    private void CorrectSizeContent() => _componentTMPForText.fontSize = _sizeContent;
    private void CorrectColorContent() => _componentTMPForText.color = _colorContent;

    private static void AddCanvas()
    {
        _canvasObject = new GameObject("Canvas for buttons", typeof(Canvas) , typeof(RectTransform), typeof(GraphicRaycaster));
        _canvasObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
    }

    private void CreateButtonKey()
    {
        CreateButton();
        CreateContentForButton();
    }

    private void CreateButton()
    {
        _button = new GameObject("Button", typeof(Button), typeof(CanvasRenderer), typeof(Image));
        _button.transform.SetParent(_canvasObject.transform);
        CorrectParamButton();
    }

    private void CorrectParamButton()
    {
        CorrectPosition();
        CorrectScaleBox();
        CorrectColorBox();
    }

    private void CreateContentForButton()
    {
        _textForButton = new GameObject("TMP");
        _componentTMPForText = _textForButton.AddComponent<TextMeshProUGUI>();
        _textForButton.transform.SetParent(_button.transform);
        _componentTMPForText.alignment = TextAlignmentOptions.Center;
        CorrectParamTextForButton();
    }

    private void CorrectParamTextForButton()
    {
        CorrectContent();
        CorrectSizeContent();
        CorrectColorContent();
    }
    
    internal void SetActive(bool active) => _button.SetActive(active);

}