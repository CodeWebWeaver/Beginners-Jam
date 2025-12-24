using System;
using UnityEngine;
using Zenject;

public class GameController : IDisposable {
    private IGameManager _gameManager;
    private InputManager inputManager;

    private InputSystem_Actions.PlayerActions actions;

    public GameController(IGameManager gameManager, InputManager inputManager) {
        _gameManager = gameManager;
        actions = inputManager.InputActions.Player;

        // Підписуємося на метод безпосередньо
        actions.Escape.performed += HandleCancel;
    }

    private void HandleCancel(UnityEngine.InputSystem.InputAction.CallbackContext ctx) {
        _gameManager.TogglePause();
    }

    public void Dispose() {
        actions.Escape.performed -= HandleCancel;
    }
}
