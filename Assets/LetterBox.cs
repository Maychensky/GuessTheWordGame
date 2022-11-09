using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WordGame
{

    internal enum StateLetterBox
    {
        Opened,
        Hidden,
    }

    internal class LetterBox 
    {
        private const char DEFOLT_CONTENT = ' ';
        private const float DEFOLT_SIZE_CONTENT = 24f;
        private static Sprite _defoltSprite;
        private GameObject _box;
        private bool _isActive;
        private bool _isOpened;
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
        internal Color colorBoxOpened { get; set; }
        internal Color colorBoxHidden { get; set; }
        internal LetterBox () : this (Vector3.zero, Vector2.one, _defoltSprite, Color.white, Color.gray, DEFOLT_CONTENT, DEFOLT_SIZE_CONTENT, Color.black) { }
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
        private void CreateLetterBox()
        {
            CreateBox();
            CreateContentForBox();
        }
        private void CreateBox()
        {
            _box = new GameObject("box", typeof(CanvasRenderer), typeof(RectTransform), typeof(Image));
            CreateParamsBox();
        }
        private void CreateParamsBox()
        {
            CorrectScaleBox();
            CorrectPosition();
        }
        private void CreateContentForBox()
        {
            _textForBox = new GameObject("TMP", typeof (TextMeshProUGUI));
            _componentTMPForText = _textForBox.GetComponent<TextMeshProUGUI>();
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
        internal void SetActive(bool active) 
        {
            _box.SetActive(active);
            _isActive = active;
        } 
        internal bool IsActive() => _isActive;
        internal void OpenedBox() 
        {
            _box.GetComponent<Image>().color = colorBoxOpened;
            _textForBox.SetActive(true);
            _isOpened = true;
        }
        internal void HiddenBox() 
        {
            _box.GetComponent<Image>().color = colorBoxHidden;
            _textForBox.SetActive(false);
            _isOpened = false;
        }
        internal bool IsOpened() => _isOpened;
        internal bool IsHidden() => !IsOpened();
        internal void SetParent(GameObject parentObject) => _box.transform.SetParent(parentObject.transform);
    }
}