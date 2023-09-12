using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private JoystickController joystickController;
    [SerializeField] private PlayerAnimator PlayerAnimator;
    private CharacterController characterController;
    Vector3 moveVector;
    [SerializeField] private int moveSpeed;

    private float gravity = -9.81f;
    private float gravityMultiplier = 3f;
    private float gravityVelocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void MovePlayer()
    {
        moveVector = joystickController.GetMovePosition() * moveSpeed * Time.deltaTime / Screen.width;


        moveVector.z = moveVector.y;
        moveVector.y = 0;

        PlayerAnimator.ManageAnimation(moveVector);
        ApplyGravity();
        characterController.Move(moveVector);
    }
    private void Update()
    {
        MovePlayer();
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded&&gravityVelocity<0.00f)
        {
            gravityVelocity = -1f;
        }
        else
        {
            gravityVelocity += gravity * gravityMultiplier * Time.deltaTime;
           
        }
        moveVector.y = gravityVelocity;
    }


}
