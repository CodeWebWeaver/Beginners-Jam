public class InputManager {
    public InputSystem_Actions InputActions { get; private set; }
    public InputManager() {
        InputActions = new InputSystem_Actions();
        InputActions.Enable();
    }
}