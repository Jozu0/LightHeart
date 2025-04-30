using System;
using Cube;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeInputHandler : InputHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
// Start is called once before the first execution of Update after the MonoBehaviour is created   
    [SerializeField] private CubeBehavior cubeBehavior;
      
    protected override void RegisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Interact"].started += OnInteractStarted;

        }
        else
        {
            Debug.LogError("PlayerInput is null in CubeInputHandler");
        }
    }

    protected override void UnregisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Interact"].started -= OnInteractStarted;
        }
    }


    private void OnInteractStarted(InputAction.CallbackContext context)
    {
        cubeBehavior.Interact();
    }
}
