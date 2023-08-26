using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private TMP_InputField _nameInputField;

    [SerializeField] private GameObject _scoreBoard;
    [SerializeField] private GameObject _mainMenu;
    private void Start()
    {
        _nameInputField.text = string.Empty;
        _startGameButton.interactable = false;
        
    }

    private void OnEnable()
    {
        _nameInputField.onEndEdit.AddListener(UpdateStartButtonState);
    }

    private void OnDisable()
    {
        _nameInputField.onEndEdit.RemoveListener(UpdateStartButtonState);
    }

    public void StartGame()
    {
        GameInstance.CurrentPlayerName = _nameInputField.text;
        
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void OpenScoreBoard()
    {
        _scoreBoard.gameObject.SetActive(true);
        _mainMenu.gameObject.SetActive(false);
    }
    public void CloseScoreBoard()
    {
        _scoreBoard.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(true);
    }

    private void UpdateStartButtonState(string inputText)
    {
        if (_nameInputField.text == string.Empty && _startGameButton.interactable)
        {
            _startGameButton.interactable = false;
        }
        else if (_nameInputField.text != string.Empty && !_startGameButton.interactable)
        {
            _startGameButton.interactable = true;
        }
    }
}
