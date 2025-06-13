using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class uranjanje_u_krst : MonoBehaviour
{
    [Header("Player Settings")]
    public GameObject player;

    [Header("UI Elements")]
    public GameObject promptText;
    public Image blackOverlay;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip splashSound;

    [Header("Door Settings")]
    public Transform doorToOpen;
    public Vector3 openRotation = new Vector3(0f, 90f, 0f);
    public float rotationSpeed = 45f;

    [Header("Interaction Settings")]
    public float immersionDuration = 3f;
    public string useButton = "Use";
    public int requiredUses = 3;

    private bool playerInRange = false;
    private bool isImmersing = false;
    private Collider playerCollider;
    private int useCount = 0;
    private bool doorOpened = false;

    void Start()
    {
        if (player != null)
            playerCollider = player.GetComponent<Collider>();
        else
            Debug.LogWarning("Player nije dodijeljen!");

        InitializeUIElements();
    }

    void InitializeUIElements()
    {
        if (promptText != null) promptText.SetActive(false);
        if (blackOverlay != null)
        {
            blackOverlay.gameObject.SetActive(false);
            blackOverlay.color = Color.black;
        }
    }

    void Update()
    {
        if (playerInRange && !isImmersing && Input.GetButtonDown(useButton))
        {
            StartCoroutine(ImmersionSequence());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsPlayerCollider(other))
        {
            playerInRange = true;
            ShowPrompt();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsPlayerCollider(other))
        {
            playerInRange = false;
            HidePrompt();
        }
    }

    bool IsPlayerCollider(Collider other)
    {
        return playerCollider != null && other == playerCollider;
    }

    IEnumerator ImmersionSequence()
    {
        isImmersing = true;
        HidePrompt();

        // Pokreni efekat uranjanja
        StartCoroutine(BlackOverlayEffect());
        PlaySplashSound();

        yield return new WaitForSeconds(immersionDuration);

        useCount++;
        CheckDoorOpening();

        isImmersing = false;
        if (playerInRange) ShowPrompt();
    }

    IEnumerator BlackOverlayEffect()
    {
        if (blackOverlay != null)
        {
            blackOverlay.gameObject.SetActive(true);
            yield return new WaitForSeconds(immersionDuration);
            blackOverlay.gameObject.SetActive(false);
        }
    }

    void PlaySplashSound()
    {
        if (audioSource != null && splashSound != null)
        {
            audioSource.PlayOneShot(splashSound);
        }
    }

    void CheckDoorOpening()
    {
        if (!doorOpened && useCount >= requiredUses && doorToOpen != null)
        {
            StartCoroutine(OpenDoor());
            doorOpened = true;
        }
    }

    IEnumerator OpenDoor()
    {
        Quaternion startRotation = doorToOpen.rotation;
        Quaternion targetRotation = Quaternion.Euler(openRotation);

        while (doorToOpen.rotation != targetRotation)
        {
            doorToOpen.rotation = Quaternion.RotateTowards(
                doorToOpen.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
            yield return null;
        }
    }

    void ShowPrompt()
    {
        if (promptText != null)
        {
            promptText.SetActive(true);
            UpdatePromptText();
        }
    }

    void UpdatePromptText()
    {
        Text textComponent = promptText.GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = $"Press {useButton.ToUpper()} to use ({useCount}/{requiredUses})";
        }
    }

    void HidePrompt()
    {
        if (promptText != null)
            promptText.SetActive(false);
    }
}
