using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [Header("Character Stats")]
    [Range(1,100)][SerializeField] float playerSpeed = 50.0f;
    [Range(5, 30)][SerializeField] float jumpHeight = 1.0f;
    [Range(-50, 50)][SerializeField] float gravityValue = -9.81f;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3();
        move += transform.forward * Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
        move += transform.right * Input.GetAxis("Horizontal") *playerSpeed * Time.deltaTime;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
