using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour {
    [Inject] private IStateMachine stateMachine;
    
    private void Start() {
        stateMachine.ChangeState<BootstrapState>();
    }

    private void OnApplicationQuit() {
        stateMachine.ChangeState<ExitState>();
    }
}