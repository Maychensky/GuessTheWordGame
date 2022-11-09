using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace WordGame
{
    internal class TextFileHandler
    {
        private const int DEFOLT_MIN_SIZE_WORD = 0;
        readonly string PATH_TO_TEXT_FILE;
        readonly int MIN_SIZE_WORD;
        private StreamReader _textFile;
        private MatchCollection _curretLine;
        private HashSet<int> _wordUsed;
        private Regex _regexForWord;
        private int _indexWordForLine;
        private readonly string PATTERN_FOR_WORD; 
        private TextFileHandler() {}
        internal TextFileHandler(string pathToTextFile) : this (pathToTextFile, DEFOLT_MIN_SIZE_WORD) {}
        internal TextFileHandler(string pathToTextFile, int minSizeWord)
        {
            PATH_TO_TEXT_FILE = pathToTextFile;
            MIN_SIZE_WORD = minSizeWord;
            _wordUsed = new HashSet<int>();
            _textFile = new StreamReader(PATH_TO_TEXT_FILE);
            PATTERN_FOR_WORD = @"[a-z]+";
            Debug.Log(PATTERN_FOR_WORD);
            CreateRegexForWord();
            _curretLine = GetNextLine();
        }
        private void CreateRegexForWord()
        {
            _regexForWord = new Regex(PATTERN_FOR_WORD, RegexOptions.IgnoreCase);
        }
        internal void ClearHistoryUsedWords()
        {
            _wordUsed.Clear();
        }
        private MatchCollection GetNextLine()
        {
            if(!_textFile.EndOfStream)
                _curretLine = _regexForWord.Matches(_textFile.ReadLine());
            return _curretLine;
        }
        private string NextWord()
        {
            if (_indexWordForLine == _curretLine.Count || _curretLine.Count == 0)
            {
                _curretLine = GetNextLine();
                _indexWordForLine = 0;
                return NextWord();
            }
            else if (_curretLine[_indexWordForLine].Length < MIN_SIZE_WORD) 
                return  NextWord();
            else
                return _curretLine[_indexWordForLine ++].ToString();

        }
        internal string GetNextWord()
        {
            var nextWord = NextWord();
            if (CheckWordInUsedWords(nextWord))
                return GetNextWord();
            else 
            {
                AddInUsedWords(nextWord);
                return nextWord;;
            }
        }
        private bool CheckWordInUsedWords(string word)
        {
            return _wordUsed.Contains(Animator.StringToHash(word));
        }
        private void AddInUsedWords(string usedWord)
        {
            _wordUsed.Add(Animator.StringToHash(usedWord));
        }
    }
}