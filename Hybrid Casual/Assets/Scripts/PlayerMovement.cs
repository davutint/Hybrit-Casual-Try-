using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private JoystickController joystickController;
    private CharacterController characterController;
    Vector3 moveVector;
    [SerializeField] private int moveSpeed;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void MovePlayer()
    {
        moveVector = joystickController.GetMovePosition() * moveSpeed * Time.deltaTime / Screen.width;


        moveVector.z = moveVector.y;
        moveVector.y = 0;

        characterController.Move(moveVector);
    }
    private void Update()
    {
        MovePlayer();
    }
}
