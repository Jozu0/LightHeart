using UnityEngine;
using UnityEngine.InputSystem;

public class ActionInputHandler : InputHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private PlayerAction playerAction;
      
    protected override void RegisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Grab"].performed += OnGrabPerformed;
            playerInput.actions["Grab"].canceled += OnGrabCanceled;
            playerInput.actions["Heal"].started += OnHealStarted;
            playerInput.actions["Attack"].started += OnAttackStarted;
        }
        else
        {
            Debug.LogError("PlayerInput is null in MovementInputHandler");
        }
    }

    protected override void UnregisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Grab"].performed -= OnGrabPerformed;
            playerInput.actions["Grab"].canceled -= OnGrabCanceled;
            playerInput.actions["Heal"].started -= OnHealStarted;
            playerInput.actions["Attack"].started -= OnAttackStarted;

        }
    }


    private void OnGrabPerformed(InputAction.CallbackContext context)
    {
        playerAction.OnGrab();
    }

    private void OnGrabCanceled(InputAction.CallbackContext context)
    {
        playerAction.OffGrab();
    }

    
    private void OnHealStarted(InputAction.CallbackContext context)
    {
        playerAction.HealCube();
    }
    
    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        playerAction.Attack();
    }

}
