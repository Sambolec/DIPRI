using UnityEngine;
using UnityEngine.InputSystem;

public class MouseThirdPersonLookPivot : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 150f;
    public float minY = -30f;
    public float maxY = 60f;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity * Time.deltaTime;
        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity * Time.deltaTime;

        // Rotacija lika oko Y
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotacija pivota oko X (kamera gore/dolje)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minY, maxY);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
