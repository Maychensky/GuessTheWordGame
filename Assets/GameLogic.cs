using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

namespace WordGame
{

    public class GameLogic : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("add start params")]
        public ParamsForWordGame startParams;
        private GameObject _canvasObject;
        private GameObject _canvasForGameOver;
        private GameObject _objectTextGameOver;
        private GameObject _buttonGameOver;
        private const string TEXT_FOR_GAME_OVER = "GAME OVER. \n TAP ON THE SCREEN...";
        private Word _word;
        private Kayboard _kayboard;
        private int _minSizeWord;
        private TextFileHandler _textFileHandler;
        private int _startingNumberTrying;
        private ControllerTrying _controllerTrying;
        private delegate void DelegateForDownNewButton(bool gotOpenedBox);
        private event DelegateForDownNewButton EventDownButton;

        void Awake()
        {
            _startingNumberTrying = 5; 
            CreateCanvas();
            CreateCanvasForGameOver();
            CreateButtonForGameOver();
            CreateMessageForGameOver();
            CreateStartesResurs();
            CreateEvents();
            _canvasForGameOver.SetActive(false);
        }
        private void CreateEvents()
        {
            AddEventButtonsClick();
            AddEventOpenedWord();
            AddEventDownButton();
            AddEventNegativeTrying();
        }
        void Start()
        {
            AddOnClickForGameOver();
            StartGame();
        }
        private void AddOnClickForGameOver()
        {
            _buttonGameOver.GetComponent<Button>().onClick.AddListener(ReserGame);
        }
        private void StartGame()
        {
            var nextWord = _textFileHandler.GetNextWord();
            _word.ChangeWord(nextWord);
        }
        private void CreateMessageForGameOver()
        {
            _objectTextGameOver = new GameObject("Game Over text", typeof(TextMeshProUGUI));
            var TMProGameOver = _objectTextGameOver.GetComponent<TextMeshProUGUI>();
            TMProGameOver.text = TEXT_FOR_GAME_OVER;
            TMProGameOver.fontSize = 1f;
            TMProGameOver.alignment = TextAlignmentOptions.Midline;
            _objectTextGameOver.transform.SetParent(_buttonGameOver.transform);
        }
        private void CreateButtonForGameOver()
        {
            _buttonGameOver = new GameObject("button the game over", typeof(Button), typeof(CanvasRenderer), typeof(Image));
            _buttonGameOver.transform.SetParent(_canvasForGameOver.transform);
            _buttonGameOver.GetComponent<Image>().color = Color.clear;
            _buttonGameOver.transform.position = new Vector3();
        }
        private void ReserGame()
        {
            Debug.Log("я тут");
            ChangeActiveCanvases(_canvasForGameOver, _canvasObject);
            _textFileHandler.ClearHistoryUsedWords();
            _controllerTrying.numberTrying = _startingNumberTrying;
            EventOpenedWord();
        }
        private void AddEventButtonsClick()
        {
            _kayboard.SetActionForButtonClick(OnClickButtonKayboard);
        }
        private void OnClickButtonKayboard(ButtonKay pressedButtonKay)
        {
            pressedButtonKay.SetActive(false);
            var gotOpenedBox = _word.OppenedLetter(pressedButtonKay.content);
            EventDownButton?.Invoke(gotOpenedBox);
        }
        private void AddEventDownButton()
        {
            EventDownButton += ChangeTryingScore;
        }
        private void AddEventNegativeTrying()
        {
            _controllerTrying.EventNegativeTrying += NegativeTrying;
        }
        private void NegativeTrying()
        {
            GameOver();
        }
        private void GameOver()
        {
            ChangeActiveCanvases(_canvasObject, _canvasForGameOver);
        }
        internal void ChangeActiveCanvases(GameObject activeCanvas, GameObject offCanvas)
        {
            activeCanvas.SetActive(false);
            offCanvas.SetActive(true);
        }
        private void ChangeTryingScore(bool gotOpenedBox)
        {
            _controllerTrying.numberTrying += (gotOpenedBox) ? 0 : -1;
        }
        private void AddEventOpenedWord()
        {
            _word.EventOpenedWord += EventOpenedWord;
            _word.EventOpenedWord += SummTrying;
        }
        private void SummTrying()
        {
            _controllerTrying.numberTrying += _startingNumberTrying;
        }
        private void EventOpenedWord()
        {
            _kayboard.DefoltKayboard();
            _word.ChangeWord(_textFileHandler.GetNextWord());
        }
        private void CreateCanvas()
        {
            _canvasObject = new GameObject("Main canvas", typeof(Canvas), typeof(GraphicRaycaster), typeof(VerticalLayoutGroup));
            _canvasObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        }
        private void CreateCanvasForGameOver()
        {
            _canvasForGameOver = new GameObject("Main canvas", typeof(Canvas), typeof(GraphicRaycaster));
        }
        private void CreateStartesResurs()
        {
            _controllerTrying = new ControllerTrying(_startingNumberTrying);
            _controllerTrying.SetParent(_canvasObject);
            _word = new Word(_canvasObject);
            _kayboard = new Kayboard(_canvasObject);
            var pathTextFile = AssetDatabase.GetAssetPath(Resources.Load("TextForWords"));
            _textFileHandler = new TextFileHandler(pathTextFile, _minSizeWord);
        }
    }
}