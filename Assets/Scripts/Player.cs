using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

    
    private int _currentScore = 0;
    public int Score
    {
        get => _currentScore;
    }

    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> ScoreChanged;
    public event UnityAction Died;


    private void Start()
    {
        HealthChanged?.Invoke(_health);
        ScoreChanged?.Invoke(_currentScore);
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        HealthChanged?.Invoke(_health);

        if (_health <= 0)
            Die();
    }

    public void AddScore(int score)
    {
        _currentScore += score;
        ScoreChanged?.Invoke(_currentScore);
    }

    public void Die()
    {
        Died?.Invoke();
    }
}
