using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private ObjectPool _objectPool;

    private List<GameObject> _objectsToRestoreAfterPause = new List<GameObject>();

    private void OnEnable()
    {
        _pauseMenu.OnGamePaused += PauseGame;
        _pauseMenu.OnGameResumed += ResumeGame;
        _pauseMenu.OnGameEnded += LoadMenuScene;
        _player.Died += LoadMenuScene;
    }

    private void OnDisable()
    {
        _pauseMenu.OnGamePaused -= PauseGame;
        _pauseMenu.OnGameResumed -= ResumeGame;
        _pauseMenu.OnGameEnded -= LoadMenuScene;
        _player.Died -= LoadMenuScene;
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    private void PauseGame()
    {
        _objectPool.GetActiveObjectsFromPool(out _objectsToRestoreAfterPause);
        foreach (var balloon in _objectsToRestoreAfterPause)
        {
            balloon.gameObject.SetActive(false);
            balloon.GetComponent<EventTrigger>().enabled = false;
        }
    }

    private void ResumeGame()
    {
        foreach (var balloon in _objectsToRestoreAfterPause)
        {
            balloon.gameObject.SetActive(true);
            balloon.GetComponent<EventTrigger>().enabled = true;
        }
    }
}
