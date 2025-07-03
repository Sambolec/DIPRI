using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;
    
    // Movement control flag
    public bool canMove = true;
    public bool IsGrounded { get; private set; } // Public read-only access
    public Vector2 MoveInput => moveInput; // NEW: Public getter for moveInput

    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;
    private Camera mainCamera; // Cached camera reference

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

    void OnEnable() => inputActions.Enable();
    void OnDisable() => inputActions.Disable();

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main; // Cache camera reference
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
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;
        
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 move = right * moveInput.x + forward * moveInput.y;
        
        if (moveInput.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
                Time.deltaTime * rotationSpeed);
        }
        
        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleJump()
    {
        if (jumpInput && IsGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpInput = false;
            animator.SetTrigger("Jump");
        }
    }

    void UpdateAnimations()
    {
        animator.SetFloat("horizontal", moveInput.x);
        animator.SetFloat("vertical", moveInput.y);
        animator.SetFloat("Blend", moveInput.magnitude);
    }

    void ResetAnimations()
    {
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
