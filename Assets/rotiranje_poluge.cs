using UnityEngine;
using System.Collections;

public class rotiranje_poluge : MonoBehaviour
{
    [Header("Player Child Objekti")]
    public GameObject playerChild1;    // Povuci prvo dijete playera
    public GameObject playerChild2;    // Povuci drugo dijete playera

    [Header("UI Elementi")]
    public GameObject interactionText;  // Povuci UI tekst za interakciju

    [Header("Postavke rotacije")]
    public float rotationAmount = 45f;  // Za koliko stupnjeva će se poluga rotirati po X osi
    public float rotationSpeed = 2f;    // Brzina rotacije

    private bool playerInRange = false;
    private bool leverActivated = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject == playerChild1 || other.gameObject == playerChild2) && !leverActivated)
        {
            playerInRange = true;
            if (interactionText != null)
                interactionText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerChild1 || other.gameObject == playerChild2)
        {
            playerInRange = false;
            if (interactionText != null)
                interactionText.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && !leverActivated && Input.GetButtonDown("Use"))
        {
            leverActivated = true;
            if (interactionText != null)
                interactionText.SetActive(false);

            StartCoroutine(RotateLever());
        }
    }

    private IEnumerator RotateLever()
    {
        Quaternion startRot = transform.rotation;
        Quaternion targetRot = startRot * Quaternion.Euler(rotationAmount, 0, 0);
        float elapsed = 0f;
        float duration = 1f / rotationSpeed;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRot, targetRot, elapsed / duration);
            yield return null;
        }
        transform.rotation = targetRot;
    }
}
