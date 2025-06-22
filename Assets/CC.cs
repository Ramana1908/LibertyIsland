using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController Controller;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Jumpforce;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Gravity = -9.81f;

    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), GetVerticalInput(), Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
    }

    private float GetVerticalInput()
    {
        float verticalInput = 0f;
        if (Input.GetKey(KeyCode.E))
        {
            verticalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            verticalInput = -1f;
        }
        return verticalInput;
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);

        if (Controller.isGrounded)
        {
            Velocity.y = -1f;
        }
        else
        {
            Velocity.y -= Gravity * 2f * Time.deltaTime;
        }

        Controller.Move(MoveVector * Speed * Time.deltaTime);
        Controller.Move(Velocity * Time.deltaTime);
    }

    private void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y * Sensitivity;
        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}
