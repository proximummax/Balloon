using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
   
    public UnityAction OnGameEnded;
    public UnityAction OnGamePaused;
    public UnityAction OnGameResumed;
    public void PauseGame()
    {
        OnGamePaused?.Invoke();

        _pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void ResumeGame()
    {
        OnGameResumed?.Invoke();

        gameObject.SetActive(false);
        _pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OpenGameMenu()
    {
        OnGameEnded?.Invoke();
    }
}
