using TMPro;
using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private TextMeshProUGUI _scoreValue;
    private void OnEnable()
    {
        _player.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _player.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int value)
    {
        _scoreValue.text = value.ToString();
    }
}
