using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CatcherManager catcherManager;
    [Space]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private Animator animator;
    [SerializeField] Transform handTransform;
    [Space]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [Space]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask solidMask;

    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, solidMask);

        if (isGrounded && velocity.y < 0)
        {
            animator.SetBool("IsJumping", false);
            velocity.y = -2f;
        } 

        if (!isGrounded && velocity.y < -2)
        {
            animator.SetBool("IsFalling", true);
        } else
        {
            animator.SetBool("IsFalling", false);
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("IsMoving", true);

            // Calculate the angle between the two axises
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        } else
        {
            animator.SetBool("IsMoving", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("IsJumping", true);

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        catcherManager.SpawnCatcher(handTransform.position, transform.rotation, gameObject);
    }
}
