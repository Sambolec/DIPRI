using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Drag your character here in the Inspector
    public Vector3 offset = new Vector3(0, 2, -4);
    public float sensitivity = 2f;

    private InputSystem_Actions inputActions;
    private Vector2 lookInput;
    private float yaw;
    private float pitch;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
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
        // Initialize yaw and pitch from current rotation
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void Update()
    {
        // Read look input every frame for smoothness
        lookInput = inputActions.Player.Look.ReadValue<Vector2>();
    }

    void LateUpdate()
    {
        if (target == null) return;

        yaw += lookInput.x * sensitivity * Time.deltaTime;
        pitch -= lookInput.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;
        transform.position = desiredPosition;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
