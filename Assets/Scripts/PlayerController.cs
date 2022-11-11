using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NPCManager NPCManager;
    [SerializeField] private CatcherManager CatcherManager;
    [SerializeField] private TaskManager TaskManager;
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

    private int points = 0;
    private Task task;
    private bool controlsMenuOpen = false;

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

        // Uncomment for debugging purposes
        // if (Input.GetKeyDown(KeyCode.N)) RefreshSpawns();

        // Respawn player if it moved out of bounds
        if (!bounds.Contains(transform.position)) Respawn();

        if (Input.GetKeyDown(KeyCode.C)) ToggleControlsMenu();

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
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
        // Calculate jump height, including moving and sprinting multipliers
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

        RefreshTask();
    }

    /// <summary>
    /// Shoot a catcher from the player's hand.
    /// </summary>
    private void Shoot()
    {
        CatcherManager.SpawnCatcher(handTransform.position, transform.rotation, gameObject);
    }

    /// <summary>
    /// Debugging method used to reoccupy all empty spawnpoints.
    /// </summary>
    private void RefreshSpawns()
    {
        foreach (var (prefab, transforms) in SpawnPoint.spawnPoints)
        {
            foreach (SpawnPoint spawn in transforms)
            {
                if (spawn.GetOccupant() == null)
                {
                    NPCManager.SpawnNPC(gameObject, spawn);
                }
            }
        }
    }

    /// <summary>
    /// Toggle the controls menu.
    /// </summary>
    public void ToggleControlsMenu()
    {
        controlsMenuOpen = !controlsMenuOpen;
    }

    /// <summary>
    /// Get the current state of the controls menu.
    /// </summary>
    /// <returns></returns>
    public bool ControlsMenuOpen()
    {
        return controlsMenuOpen;
    }

    /// <summary>
    /// Get the player's points.
    /// </summary>
    /// <returns>The player's points</returns>
    public int Points()
    {
        return points;
    }

    /// <summary>
    /// Set the player's points.
    /// </summary>
    /// <param name="points">The points to set to.</param>
    /// <returns>The updated point amount</returns>
    public int Points(int points)
    {
        this.points = points;
        return this.points;
    }

    /// <summary>
    /// Change the player's points by a given amount.
    /// </summary>
    /// <param name="points"></param>
    /// <returns>The updated point amount</returns>
    public int ChangePoints(int points)
    {
        int newPoints = points + Points();
        Points(newPoints);

        return newPoints;
    }

    /// <summary>
    /// Refesh the player's task.
    /// </summary>
    /// <returns>The new task</returns>
    public Task RefreshTask()
    {
        task = TaskManager.GenerateTask();
        return task;
    }

    /// <summary>
    /// Get the player's current task.
    /// </summary>
    /// <returns>The player's current task</returns>
    public Task CurrentTask()
    {
        return task;
    }
}
