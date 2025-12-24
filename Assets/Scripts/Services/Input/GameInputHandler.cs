using UnityEngine;
using Zenject;

public class GameInputHandler : MonoBehaviour {
    [Inject] private IGameManager _gameManager;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            _gameManager.PauseGame();
        }
    }
}