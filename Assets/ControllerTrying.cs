using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WordGame
{
    internal class ControllerTrying
    {
        private const string DEFOLT_NAME_FOR_SCORE = "TRYING: ";
        private readonly string NAME_FOR_SCORE;
        private GameObject _objectForTextName;
        private GameObject _objectForText;
        private TextMeshProUGUI _componentTMPForText;
        private GameObject _panelForText;
        private int _startingNumberTrying;
        private int _numberTrying;
        internal int numberTrying
        {
            get {return _numberTrying; }
            set {_numberTrying = value; CorrectText(); CheckNegativeValue(); }
        }
        private Vector3 _position;
        internal Vector3 position
        {
            get {return _position; }
            set {_position = value; CorrectPosition(); }
        }
        internal delegate void DelegateForNegativeTrying();
        internal event DelegateForNegativeTrying EventNegativeTrying;
        private ControllerTrying () {}
        internal ControllerTrying(int startingNumberTrying) : this (startingNumberTrying, DEFOLT_NAME_FOR_SCORE) { } 
        internal ControllerTrying(int startingNumberTrying, string nameForScore)
        {
            NAME_FOR_SCORE = nameForScore;
            _startingNumberTrying = startingNumberTrying;
            _numberTrying = startingNumberTrying;
            CreateText();
            CreateTextName();
            CreatePanel();
            SetParentTextAnPanel();
        }
        private void CheckNegativeValue()
        {
            if (_numberTrying < 0)
                EventNegativeTrying?.Invoke();
        }
        private void SetParentTextAnPanel()
        {
            _objectForTextName.transform.SetParent(_panelForText.transform);
            _objectForText.transform.SetParent(_panelForText.transform);
        }
        private void CreateTextName()
        {
            _objectForTextName = GameObject.Instantiate(_objectForText);
            _objectForTextName.GetComponent<TextMeshProUGUI>().text = NAME_FOR_SCORE;
        }
        internal void SetParent(GameObject parentObject)
        {
            _panelForText.transform.SetParent(parentObject.transform);
        }
        private void CreateText()
        {
            _objectForText = new GameObject("TMP");
            _componentTMPForText = _objectForText.AddComponent<TextMeshProUGUI>();
            _componentTMPForText.alignment = TextAlignmentOptions.TopRight;
            CorrectPosition();
            CorrectText();
        }
        private void CreatePanel()
        {
            _panelForText = new GameObject("Panel for kayboard", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(HorizontalLayoutGroup));
            _panelForText.GetComponent<Image>().color = Color.clear;
            CorrectPeramsGridLayoutGroup();
        }
        private void CorrectPeramsGridLayoutGroup()
        {
            HorizontalLayoutGroup uiHorizontalLayoutGroup = _panelForText.GetComponent<HorizontalLayoutGroup>();
            uiHorizontalLayoutGroup.childAlignment = TextAnchor.UpperRight;
            uiHorizontalLayoutGroup.childForceExpandWidth = false;
            uiHorizontalLayoutGroup.padding.right = 10;
        }
        private void CorrectText()
        {
            _componentTMPForText.text = _numberTrying.ToString();
        }
        private void CorrectPosition() => _objectForText.transform.position = _position;
    }
}