using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour, IUiManager {
    [SerializeField] TextMeshProUGUI messageText;
    public void ShowMessage(string message) {
        messageText.text = message;
    }
}