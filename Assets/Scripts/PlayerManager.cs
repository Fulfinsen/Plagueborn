using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    [Header("Input Action Asset")] 
    [SerializeField] InputActionAsset playerControls;
    
    [Header("Action Map Name Reference")]
    [SerializeField] string actionMapName = "Player";
    
    [Header("Action Name Reference")]
    [SerializeField] string moveActionName = "Move";
    [SerializeField] string lookActionName = "Look";
    [SerializeField] string jumpActionName = "Jump";
    [SerializeField] string sprintActionName = "Sprint";
    
    InputAction moveAction;
    InputAction lookAction;
    InputAction jumpAction;
    InputAction sprintAction;
    
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool jumpTriggered { get; private set; }
    public float sprintValue { get; private set; }
    
    public static PlayerManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(moveActionName);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(lookActionName);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jumpActionName);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprintActionName);
        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => moveInput = Vector2.zero;
        
        lookAction.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        lookAction.canceled += ctx => lookInput = Vector2.zero;
        
        jumpAction.performed += ctx => jumpTriggered = true;
        jumpAction.canceled += ctx => jumpTriggered = false;
        
        sprintAction.performed += ctx => sprintValue = ctx.ReadValue<float>();
        sprintAction.canceled += ctx => sprintValue = 0;
    }

    void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
    }
}
