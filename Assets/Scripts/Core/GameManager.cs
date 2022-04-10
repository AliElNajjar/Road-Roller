using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int prototypeSceneIndex = 1;

    public enum GameState
    {
        Pregame,
        Running,
        Paused,
        Ended
    }

    [HideInInspector] public EventManager.EventGameState OnGameStateChanged;

    private GameState _currentGameState = GameState.Pregame;


    private void Start()
    {
        EventManager.OnPauseToggle += TogglePause;
        EventManager.OnStartRequested += StartGame;
        EventManager.OnRestartRequested += RestartLevel;
        EventManager.OnLevelEnd += EndLevel;
        EventManager.OnQuitRequested += QuitGame;
        LoadLevel(prototypeSceneIndex);
    }

    private void UpdateState(GameState state)
    {
        _currentGameState = state;

        if (state == GameState.Pregame || state == GameState.Running) Time.timeScale = 1f;
        if (state == GameState.Paused || state == GameState.Ended) Time.timeScale = 0f;

        OnGameStateChanged.Invoke(_currentGameState);
    }

    private void RestartLevel()
    {
        if (SceneManager.GetSceneByBuildIndex(prototypeSceneIndex).isLoaded)
        {
            SceneManager.UnloadSceneAsync(prototypeSceneIndex);
        }

        UpdateState(GameState.Pregame);

        LoadLevel(prototypeSceneIndex);
    }

    private void StartGame()
    {
        UpdateState(GameState.Running);
    }


    private void TogglePause()
    {
        UpdateState(_currentGameState == GameState.Running ? GameState.Paused : GameState.Running);
    }

    private void EndLevel()
    {
        UpdateState(GameState.Ended);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void LoadLevel(int index)
    {
        SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
    }
}
