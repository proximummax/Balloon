using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private TMP_InputField _inputField;

    [SerializeField] private DataManager _dataManager;

    [SerializeField] private ScoreBoardElement _scoreBoardElementPrefab;

    private List<ScoreBoardElement> _scoreBoardElements = new List<ScoreBoardElement>();
    
    private void OnEnable()
    {
        _inputField.onValueChanged.AddListener(FindNameInBoardByWords);
    }

    private void OnDisable()
    {
        _inputField.onValueChanged.RemoveListener(FindNameInBoardByWords);
    }
    void Start()
    {
        _dataManager.LoadScore(out List<DataManager.SaveScoreParameters> scoresParameters);
        scoresParameters.Sort((x, y) => y.Score.CompareTo(x.Score));

        for(int i = 0; i < scoresParameters.Count; i++)
        {
           var scoreElement = Instantiate(_scoreBoardElementPrefab, _scrollRect.content);
            scoreElement.Name.text = scoresParameters[i].Name;
            scoreElement.Score.text = scoresParameters[i].Score.ToString();
            _scoreBoardElements.Add(scoreElement);
        }
    }

    private void FindNameInBoardByWords(string words)
    {
        foreach(var scoreBoardElement in _scoreBoardElements)
        {
            if (scoreBoardElement.Name.text.IndexOf(words, System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                scoreBoardElement.gameObject.SetActive(true);
            }
            else
            {
                scoreBoardElement.gameObject.SetActive(false);
            }
        }
    }
    
}
