using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    [Header("Player Input Variables")]
    [SerializeField] private PlayerMaster playerMaster;
    [SerializeField] private InputMaster playerControls;

    private void Awake()
    {
        playerMaster = GetComponent<PlayerMaster>();
        playerControls = new InputMaster();
        InitializeControls();
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();
    }

    private void InitializeControls()
    {
        playerControls.Gameplay.Movement.performed += context => playerMaster.MoveDirection = context.ReadValue<Vector2>();
        playerControls.Gameplay.Movement.canceled += context => playerMaster.MoveDirection = Vector2.zero;

        playerControls.Gameplay.Attack.performed += context => playerMaster.PlayerAttack();

        playerControls.Gameplay.Dash.performed += context => playerMaster.PlayerDash();

        playerControls.Gameplay.Interact.performed += context => playerMaster.PlayerInteractCheck();
    }
}
