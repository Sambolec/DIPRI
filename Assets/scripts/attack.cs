using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private InputSystem_Actions inputActions;

    // Combat system additions
    public SwordDamage swordDamage;       // Assign your SwordDamage script in Inspector
    public int lightAttackDamage = 25;    // Light attack damage
    public int heavyAttackDamage = 50;    // Heavy attack damage

    [Header("Movement Reference")]
    public PlayerMovement movementScript;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Light Attack")]
    public AudioClip lightAttackClip;
    public float lightAttackSoundDelay = 0f;

    [Header("Heavy Attack")]
    public AudioClip heavyAttackClip;
    public float heavyAttackSoundDelay = 0f;

    void Awake()
    {
        animator = GetComponent<Animator>();

        // Auto-find sword damage if not assigned
        if (swordDamage == null)
            swordDamage = GetComponentInChildren<SwordDamage>();

        if (movementScript == null)
            movementScript = GetComponent<PlayerMovement>();

        inputActions = new InputSystem_Actions();
        inputActions.Player.LightAttack.performed += ctx => OnLightAttack();
        inputActions.Player.HeavyAttack.performed += ctx => OnHeavyAttack();
    }

    void OnEnable() => inputActions.Enable();
    void OnDisable() => inputActions.Disable();

    void OnLightAttack()
    {
        if (movementScript != null && movementScript.canMove)
        {
            movementScript.canMove = false;
            animator.SetTrigger("LightAttack");

            // Set light attack damage
            if (swordDamage != null)
                swordDamage.SetDamage(lightAttackDamage);

            StartCoroutine(PlayAttackSoundWithDelay(lightAttackClip, lightAttackSoundDelay));
        }
    }

    void OnHeavyAttack()
    {
        if (movementScript != null && movementScript.canMove)
        {
            movementScript.canMove = false;
            animator.SetTrigger("HeavyAttack");

            // Set heavy attack damage
            if (swordDamage != null)
                swordDamage.SetDamage(heavyAttackDamage);

            StartCoroutine(PlayAttackSoundWithDelay(heavyAttackClip, heavyAttackSoundDelay));
        }
    }

    // Call these via animation events at the end of attack animations
    public void EnableMovement()
    {
        if (movementScript != null)
            movementScript.canMove = true;
    }

    // Coroutine to play the attack sound after a delay
    private IEnumerator PlayAttackSoundWithDelay(AudioClip clip, float delay)
    {
        if (audioSource != null && clip != null)
        {
            if (delay > 0f)
                yield return new WaitForSeconds(delay);

            audioSource.PlayOneShot(clip);
        }
    }
}
