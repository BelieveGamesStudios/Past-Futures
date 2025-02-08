using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    PlayerInput PlayerInput;
    InputSystem_Actions inputHandler;
    PlayerMovement movement;

    private void OnEnable()
    {
        PlayerInput = GetComponent<PlayerInput>();
        inputHandler=new InputSystem_Actions();
        inputHandler.Player.Enable();
        movement=FindFirstObjectByType<PlayerMovement>();
    }
    private void Update()
    {
        Vector2 move=inputHandler.Player.Move.ReadValue<Vector2>();
        Vector2 yaw=inputHandler.Player.Look.ReadValue<Vector2>();
        bool isRun= inputHandler.Player.Sprint.IsPressed();
        bool isJump=inputHandler.Player.Jump.IsPressed();
        movement.Move(move, isRun,isJump,yaw);
    }
}
