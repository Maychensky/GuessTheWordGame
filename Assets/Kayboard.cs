using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace WordGame
{
    internal class Kayboard
    {
        private const string ALPHABET_ENGLISH_STRING = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly char[] ALPHABET_ENGLISH_CHARS;
        private GameObject _parentObject;
        private GameObject _panelForKayboard;
        private Dictionary<char, ButtonKay> _dictionaryLettersButtonKays;
        internal Kayboard(GameObject parentObject)
        {
            _parentObject = parentObject;
            ALPHABET_ENGLISH_CHARS = ALPHABET_ENGLISH_STRING.ToCharArray();
            CreatePanel();
            InitAlphabetDictonary();
        }
        private void InitAlphabetDictonary()
        {
            _dictionaryLettersButtonKays = new Dictionary<char, ButtonKay>();
            foreach(var letter in ALPHABET_ENGLISH_CHARS)
            {
                _dictionaryLettersButtonKays.Add(letter, new ButtonKay()); 
                _dictionaryLettersButtonKays[letter].content = letter;
            }
            AddWordInPanel();
        }
        private void CreatePanel()
        {
            _panelForKayboard = new GameObject("Panel for kayboard", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(GridLayoutGroup));
            _panelForKayboard.GetComponent<Image>().color = Color.clear;
            SetParent(_parentObject);
            CorrectParamsRectTransform();
            CorrectPeramsGridLayoutGroup();
        }
        internal void SetParent(GameObject parentObject)
        {
            _panelForKayboard.transform.SetParent(parentObject.transform);
        }
        internal void SetActionForButtonClick(DelegateOnClick action)
        {
            foreach (var button in _dictionaryLettersButtonKays.Values)
                button.EventOnClick += action;
        }
        internal void DefoltKayboard()
        {
            foreach (var buttonKay in _dictionaryLettersButtonKays)
                buttonKay.Value.SetActive(true);
        }
        private void AddWordInPanel()
        {
            foreach(var button in _dictionaryLettersButtonKays.Values)
                button.SetParent(_panelForKayboard);
        }
        private void CorrectParamsRectTransform()
        {
            RectTransform uitransform = _panelForKayboard.GetComponent<RectTransform>();
            uitransform.pivot = new Vector2(0.5f, 0.5f);
            uitransform.anchorMin = Vector2.zero;
            uitransform.anchorMax = Vector2.one;
            uitransform.offsetMin = Vector2.zero;
            uitransform.offsetMax = Vector2.zero;
        }
        private void CorrectPeramsGridLayoutGroup()
        {
            GridLayoutGroup uiHorizontalLayoutGroup = _panelForKayboard.GetComponent<GridLayoutGroup>();
            uiHorizontalLayoutGroup.childAlignment = TextAnchor.UpperCenter; 
            uiHorizontalLayoutGroup.cellSize = new(40, 40);
            uiHorizontalLayoutGroup.spacing = new(20, 20);
            uiHorizontalLayoutGroup.padding.left = 40;
            uiHorizontalLayoutGroup.padding.right = 40;
            uiHorizontalLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            uiHorizontalLayoutGroup.constraintCount = 3;
        }
    }
}