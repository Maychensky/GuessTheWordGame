using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WordGame
{
    internal class Word
    {
        private GameObject _parentObject;
        private GameObject _panelForWord;
        private const int MAX_SIZE_WORD = 20;
        private List<LetterBox> _wordContainer;
        private Vector3 _position;
        internal Vector3 position
        {
            get {return _position; }
            set {_position = value; }   
        }
        internal delegate void DlegateOpenedWord(); 
        internal event DlegateOpenedWord EventOpenedWord;
        internal Word() : this (null) { }
        internal Word(GameObject parentObject)
        {
            _parentObject = parentObject;
            CreatePanel();
            CreateWord();
        }
        private void CreatePanel()
        {
            _panelForWord = new GameObject("Panel for word", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(GridLayoutGroup));
            _panelForWord.GetComponent<Image>().color = Color.clear;
            SetParent(_parentObject);
            CorrectParamsRectTransform();
            CorrectPeramsGridLayoutGroup();
        }
        private void CreateWord()
        {
            _wordContainer = new List<LetterBox>(MAX_SIZE_WORD);
            AddLetterBoxForWord(MAX_SIZE_WORD);
            OffWordContainer();
        }
        private void AddLetterBoxForWord(int count)
        {
            for(int i = 0; i < count; i ++)
                _wordContainer.Add(new LetterBox());
            AddWordInPanel();
        }
        private void AddWordInPanel()
        {
            foreach(var letter in _wordContainer)
                letter.SetParent(_panelForWord);
        }
        private void OffWordContainer()
        {  
            foreach(var letterBox in _wordContainer)
                if (letterBox != null)
                    letterBox.SetActive(false);
        }
        internal void ChangeWord(string newWord)
        {
            var sizeWord = newWord.Length;
            CheckWillItFit(sizeWord);
            CreateNewWord(newWord);
            CorrectActiveLetterBoxes(sizeWord);
            HiddenWord();
        }
        internal void HiddenWord()
        {
            foreach (var letterBox in _wordContainer)
                letterBox.HiddenBox();
        }
        private void CheckWillItFit(int sizeNextWord)
        {
            if (sizeNextWord > _wordContainer.Count)
                AddLetterBoxForWord(sizeNextWord - _wordContainer.Count);
        }
        private void CreateNewWord(string newWord)
        {
            var curretIndexWord = 0;
            foreach (var letter in newWord)
                _wordContainer[curretIndexWord ++].content = letter;
        }
        private void CorrectActiveLetterBoxes(int sizeNextWord)
        {
            for (int i = 0; i < sizeNextWord; i ++)
                _wordContainer[i].SetActive(true);
            for (int i = sizeNextWord; i < _wordContainer.Count; i ++)
                _wordContainer[i].SetActive(false);
        }
        private void CorrectParamsRectTransform()
        {
            RectTransform uitransform = _panelForWord.GetComponent<RectTransform>();
            uitransform.pivot = new Vector2(0.5f, 0.5f);
            uitransform.anchorMin = Vector2.zero;
            uitransform.anchorMax = Vector2.one;
            uitransform.offsetMin = Vector2.zero;
            uitransform.offsetMax = Vector2.zero;
        }
        private void CorrectPeramsGridLayoutGroup()
        {
            GridLayoutGroup uiHorizontalLayoutGroup = _panelForWord.GetComponent<GridLayoutGroup>();
            uiHorizontalLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
            uiHorizontalLayoutGroup.cellSize = new(60, 60);
            uiHorizontalLayoutGroup.spacing = new(15, 15);
        }
        internal void SetParent(GameObject parentObject)
        {
            _panelForWord.transform.SetParent(parentObject.transform);
        }
        internal bool OppenedLetter(char soughtСontent)
        {
            var gotOpenedBox = false;
            foreach (var letterBox in _wordContainer)
                if (letterBox.IsActive() && CharEqlipsNotRegistr(letterBox.content, soughtСontent))
                {
                    letterBox.OpenedBox();
                    gotOpenedBox = true;
                }
            CheckAllOpenedLetters();
            return gotOpenedBox;
        }
        private void CheckAllOpenedLetters()
        {
            foreach (var letterBox in _wordContainer)
                if (letterBox.IsActive() && letterBox.IsHidden())
                    return;
            ActionOpenedWord();
        }

        private void ActionOpenedWord()
        {
            EventOpenedWord?.Invoke();
        }

        private bool CharEqlipsNotRegistr(char a, char b) => (char.ToUpperInvariant(a) == char.ToUpperInvariant(b));
    }
}