using System.Collections.Generic;
using UnityEngine;

internal class Word
{
    private const int MAX_SIZE_WORD = 20;
    private List<LetterBox> _word;
    private Vector3 _position;
    internal Vector3 position
    {
        get {return _position; }
        set {_position = value; }   
    }

    internal Word()
    {
        CreateWord();
    }
    private void CreateWord()
    {
        _word = new List<LetterBox>(MAX_SIZE_WORD);
        AddLetterBoxForWord(MAX_SIZE_WORD);
        OffStartWord();
    }

    private void AddLetterBoxForWord(int count)
    {
        for(int i = 0; i < count; i ++)
            _word.Add(new LetterBox());
    }

    private void OffStartWord()
    {  
        foreach(var letterBox in _word)
            if (letterBox != null)
                letterBox.SetActive(false);
    }

    internal void ChangeWord(string newWord)
    {
        var sizeWord = newWord.Length;
        CheckWillItFit(sizeWord);
        CreateNewWord(newWord);
        CorrectActiveLetterBoxes(sizeWord);
    }

    private void CheckWillItFit(int sizeNextWord)
    {
        if (sizeNextWord > _word.Count)
            AddLetterBoxForWord(sizeNextWord - _word.Count);
    }

    private void CreateNewWord(string newWord)
    {
        var curretIndexWord = 0;
        foreach (var letter in newWord)
            _word[curretIndexWord ++].content = letter;
    }

    private void CorrectActiveLetterBoxes(int sizeNextWord)
    {
        for (int i = 0; i < sizeNextWord; i ++)
            _word[i].SetActive(true);
        for (int i = sizeNextWord; i < _word.Count; i ++)
            _word[i].SetActive(false);
    }
}