using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private bool isGrounded;

    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask solidMask;
    [SerializeField] Transform feetTransform;
    [SerializeField] Transform handTransform;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Rigidbody playerBody;
    [Space]
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float cameraAngle;
    [Space]
    [SerializeField] private Rigidbody catcherBody;
    [SerializeField] private float catcherSpeed;

    private void Update()
    {
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (playerMovementInput.x != 0 || playerMovementInput.z != 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (Physics.CheckSphere(feetTransform.position, 0.1f, solidMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), 0f);

        MovePlayer();
        MovePlayerCamera();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput) * speed;
        playerBody.velocity = new Vector3(moveVector.x, playerBody.velocity.y, moveVector.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void MovePlayerCamera()
    {
        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraAngle, 0f, 0f);
    }

    private void Shoot()
    {
        Rigidbody catcher = Instantiate(catcherBody, handTransform.position, transform.rotation);
        catcher.velocity = transform.forward * speed + transform.up * speed;
    }
}
