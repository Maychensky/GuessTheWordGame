using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WordGame
{
    internal delegate void DelegateOnClick(ButtonKay pressedButtonKay);
    internal class ButtonKay
    {
        private const char DEFOLT_CONTENT = ' ';
        private const float DEFOLT_SIZE_CONTENT = 24f;
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
            set {_content = value; CorrectContent(); }
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
        internal event DelegateOnClick EventOnClick;
        internal ButtonKay() : this( Vector3.zero, Vector2.one, Color.white, DEFOLT_CONTENT, DEFOLT_SIZE_CONTENT, Color.black) { }
        internal ButtonKay(char content): this (Vector3.zero, Vector2.one, Color.white, content, DEFOLT_SIZE_CONTENT, Color.black) { }
        internal ButtonKay(Vector3 position, Color colorBox, char content, Color colorContent) : this( position, Vector2.one, colorBox, content, DEFOLT_SIZE_CONTENT, colorContent) { }
        internal ButtonKay( Vector3 position, Vector2 scaleBox, Color colorBox, char content, float sizeContent, Color colorContent)
        {
            _position = position;
            _scaleBox = scaleBox;
            _colorBox = colorBox;
            _content = content;
            _sizeContent = sizeContent;
            _colorContent = colorContent;
            CreateButtonKey();
            CreateButtonAction();
        }
        private void CorrectPosition() => _button.transform.position = _position;
        private void CorrectScaleBox() => _button.transform.localScale = new(_scaleBox.x, _scaleBox.y, _button.transform.localScale.z);
        private void CorrectColorBox() => _button.GetComponent<Image>().color = _colorBox;
        private void CorrectContent() => _componentTMPForText.text = _content.ToString();
        private void CorrectSizeContent() => _componentTMPForText.fontSize = _sizeContent;
        private void CorrectColorContent() => _componentTMPForText.color = _colorContent;
        private void CreateButtonKey()
        {
            CreateButton();
            CreateContentForButton();
        }
        private void CreateButton()
        {
            _button = new GameObject("Button", typeof(Button), typeof(CanvasRenderer), typeof(Image));
            CorrectParamsButton();
        }
        private void CorrectParamsButton()
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
            CorrectParamsTextForButton();
        }
        private void CorrectParamsTextForButton()
        {
            CorrectContent();
            CorrectSizeContent();
            CorrectColorContent();
            CorrectParamsRectTransformForText();
        }
        private void CorrectParamsRectTransformForText()
        {
            RectTransform uitransform = _textForButton.GetComponent<RectTransform>();
            uitransform.pivot = new Vector2(0.5f, 0.5f);
            uitransform.anchorMin = Vector2.zero;
            uitransform.anchorMax = Vector2.one;
            uitransform.offsetMin = Vector2.zero;
            uitransform.offsetMax = Vector2.zero;
        }
        internal void SetActive(bool active) 
        {
            _button.GetComponent<Button>().interactable = active;
            if (active) ChangeColorButtonAndText(_colorBox, _colorContent);
            else ChangeColorButtonAndText(Color.clear, Color.clear);
        } 
        private void ChangeColorButtonAndText(Color colorButton, Color colorText)
        {
            _button.GetComponent<Image>().color = colorButton;
            _textForButton.GetComponent<TextMeshProUGUI>().color = colorText;
        }
        internal void SetParent(GameObject parentObject) => _button.transform.SetParent(parentObject.transform);
        private void CreateButtonAction()
        {
            _button.GetComponent<Button>().onClick.AddListener(OnClick);
        }
        private void OnClick()
        {
            EventOnClick?.Invoke(this);
        }
    }
}