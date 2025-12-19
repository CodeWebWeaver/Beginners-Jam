using UnityEngine;
using Zenject;

public class MyTest : MonoBehaviour
{
    [Inject] IUiManager uiManager;
    [SerializeField] string message = "Hello! Zenject Working!";

    private void Start() {
        uiManager.ShowMessage(message);
    }
}
