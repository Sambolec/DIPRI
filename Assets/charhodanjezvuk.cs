using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource; // Assign in Inspector
    public float stepInterval = 0.5f; // Time between steps
    private float stepTimer;
    private PlayerMovement movementScript;

    void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (movementScript == null || audioSource == null) return;

        // Use magnitude to detect movement in ANY direction (W/A/S/D)
        bool isMoving = movementScript.canMove && movementScript.MoveInput.magnitude > 0.1f;

        if (isMoving && movementScript.IsGrounded)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                audioSource.PlayOneShot(audioSource.clip);
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f; // Reset timer when not moving
        }
    }
}
