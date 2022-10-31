using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NPCManager NPCManager;
    [SerializeField] private CatcherManager catcherManager;
    [Space]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform spawnPoint;
    [Space]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float movingJumpMultiplier = 1.2f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float sprintJumpMultiplier = 1.2f;
    [Space]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask solidMask;
    [SerializeField] private Bounds bounds;

    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isSprinting;
    private bool isMoving;

    /// <summary>
    /// Called on creation.
    /// Used to set player variables and respawn the player.
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Respawn();
    }

    /// <summary>
    /// Called every frame.
    /// Used to update the player's movement and gravity.
    /// </summary>
    void Update()
    {
        CheckSprinting();

        CheckGrounded();

        Move();

        if (Input.GetButtonDown("Jump") && isGrounded) Jump();

        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.Mouse0)) Shoot();

        if (Input.GetKeyDown(KeyCode.N)) RefreshSpawns();

        if (!bounds.Contains(transform.position)) Respawn();
    }

    /// <summary>
    /// Check if player is sprinting.
    /// </summary>
    private void CheckSprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            animator.SetBool("IsSprinting", true);
        }
        else
        {
            isSprinting = false;
            animator.SetBool("IsSprinting", false);
        }
    }

    /// <summary>
    /// Check if player is on the ground or falling.
    /// </summary>
    private void CheckGrounded()
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
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }
    }

    /// <summary>
    /// Apply movement inputs on the player.
    /// </summary>
    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            isMoving = true;
            animator.SetBool("IsMoving", true);

            // Calculate the angle between the two axises
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime * (isSprinting ? sprintMultiplier : 1));
        }
        else
        {
            isMoving = false;
            animator.SetBool("IsMoving", false);
        }
    }

    /// <summary>
    /// Apply jumping inputs on the player.
    /// </summary>
    private void Jump()
    {
        animator.SetBool("IsJumping", true);
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * (isMoving ? movingJumpMultiplier : 1) * (isSprinting ? sprintJumpMultiplier : 1));
    }

    /// <summary>
    /// Apply gravity forces on the player.
    /// </summary>
    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// Respawn the player by teleporting to a playerspawn.
    /// </summary>
    public void Respawn()
    {
        Vector3 position = spawnPoint.transform.position;
        position.y += transform.lossyScale.y;

        transform.position = position;
        transform.rotation = spawnPoint.rotation;
    }

    /// <summary>
    /// Shoot a catcher from the player's hand.
    /// </summary>
    private void Shoot()
    {
        catcherManager.SpawnCatcher(handTransform.position, transform.rotation, gameObject);
    }

    /// <summary>
    /// Debugging method used to reoccupy all spawnpoints.
    /// </summary>
    private void RefreshSpawns()
    {
        foreach (var (prefab, transforms) in SpawnPoint.spawnPoints)
        {
            foreach (SpawnPoint spawn in transforms)
            {
                if (spawn.getOccupant() == null)
                {
                    NPCManager.SpawnNPC(gameObject, spawn);
                }
            }
        }
    }
}
