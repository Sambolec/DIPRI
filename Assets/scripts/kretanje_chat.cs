using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;

    public bool canMove = true;
    public bool IsGrounded { get; private set; }
    public Vector2 MoveInput => moveInput;

    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    private InputSystem_Actions inputActions;
    private Vector2 moveInput;
    private bool jumpInput;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Jump.performed += ctx => jumpInput = true;
    }

    void OnEnable()
    {
        inputActions.Enable();
    }
    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        IsGrounded = controller.isGrounded;
        if (IsGrounded && velocity.y < 0) velocity.y = -2f;

        if (canMove)
        {
            HandleMovement();
            HandleJump();
            UpdateAnimations();
        }
        else
        {
            ResetAnimations();
        }

        ApplyGravity();
    }

    void HandleMovement()
    {
        // Always get the current MainCamera so it's correct after switching
        Camera cam = Camera.main;
        if (cam == null) return;

        // Get camera's forward and right directions, flattened to ground
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // Camera-relative movement: WASD is always relative to camera
        Vector3 move = camForward * moveInput.y + camRight * moveInput.x;

        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleJump()
    {
        if (jumpInput && IsGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpInput = false;
            if (animator != null)
                animator.SetTrigger("Jump");
        }
    }

    void UpdateAnimations()
    {
        if (animator == null) return;
        animator.SetFloat("horizontal", moveInput.x);
        animator.SetFloat("vertical", moveInput.y);
        animator.SetFloat("Blend", moveInput.magnitude);
    }

    void ResetAnimations()
    {
        if (animator == null) return;
        animator.SetFloat("horizontal", 0);
        animator.SetFloat("vertical", 0);
        animator.SetFloat("Blend", 0);
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnDestroy()
    {
        if (inputActions != null)
            inputActions.Dispose();
    }
}
