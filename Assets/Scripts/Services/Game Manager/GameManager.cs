using System;
using UnityEngine;
using Zenject;

public class GameManager : IGameManager {

    private IStateMachine _gameStateMachine;
    private ILevelProgressService _levelProgress;

    public GameManager(IStateMachine stateMachine, ILevelProgressService levelProgress) {
        _gameStateMachine = stateMachine;
        _levelProgress = levelProgress;
    }

    public void StartNewGame() {
        Debug.Log("Game Started!");
        _levelProgress.SetProgress(new LevelProgress { CurrentLevel = 1 });
        _gameStateMachine.ChangeState<LoadingLevelState>();
    }

    public void ContinueGame() {
        _gameStateMachine.ChangeState<LoadingLevelState>();
    }

    #region Pause/Resume
    public void TogglePause() {
        bool isPaused = _gameStateMachine.CurrentState is PauseState;
        if (isPaused) {
            ResumeGame();
        } else {
            PauseGame();
        }
    }

    public void PauseGame() {
        _gameStateMachine.ChangeState<PauseState>();
    }

    public void ResumeGame() {
        _gameStateMachine.ChangeState<GameLoopState>();
    }
    #endregion

    public void ExitToMainMenu() {
        _gameStateMachine.ChangeState<LoadingMainMenuState>();
    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}


