using System;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement Speed")] 
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    
    [Header("Jump Parameters")]
    [SerializeField] float jumpForce;
    [SerializeField] float gravity = 9.81f;
    
    [Header("Look Sensitivity")]
    [SerializeField] float mouseSensitivity;
    [SerializeField] float upDownRange = 80f;
    
    CharacterController characterController;
    Camera mainCamera;
    PlayerManager playerManager;
    Vector3 currentMovement = Vector3.zero;
    float verticalRotation;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        playerManager = PlayerManager.Instance;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float speed = walkSpeed * (playerManager.sprintValue > 0 ? sprintSpeed : 1f);
        
        Vector3 inputDirection = new Vector3(playerManager.moveInput.x, 0f, playerManager.moveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        worldDirection.Normalize();

        currentMovement.x = worldDirection.x * speed;
        currentMovement.z = worldDirection.z * speed;
        
        HandleJumping();
        characterController.Move(currentMovement * Time.deltaTime);
    }
    
    void HandleJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;
            if (playerManager.jumpTriggered)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }
    
    void HandleRotation()
    {
        float mouseXRotation = playerManager.lookInput.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);
        
        verticalRotation -= playerManager.lookInput.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
