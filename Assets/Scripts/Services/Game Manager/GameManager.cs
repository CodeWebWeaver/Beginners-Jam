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
        // Load current level or initialize game state here
        _levelProgress.SetProgress(new LevelProgress { CurrentLevel = 1 });
        _gameStateMachine.ChangeState<LoadingLevelState>();
    }

    private void ProceedToLevel(int level) {
        // Logic to load the specified level
        Debug.Log($"Loading Level {level}...");
    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenMenu() {
        _gameStateMachine.ChangeState<LoadingMainMenuState>();
    }
}


