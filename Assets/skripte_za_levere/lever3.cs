using UnityEngine;
using System.Collections;

public class lever3 : MonoBehaviour
{
    [Header("Player Child Objekti")]
    public GameObject playerChild1;    // Povuci prvo dijete playera
    public GameObject playerChild2;    // Povuci drugo dijete playera

    [Header("UI Elementi")]
    public GameObject interactionText;  // Povuci UI tekst za interakciju

    [Header("Postavke rotacije")]
    public float rotationAmount = 45f;  // Za koliko stupnjeva će se poluga rotirati po X osi
    public float rotationSpeed = 2f;    // Brzina rotacije

    [Header("Lever Kamera")]
    public Camera leverCamera;          // Povuci posebnu kameru koja snima polugu

    private bool playerInRange = false;
    private bool leverActivated = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.SetActive(false);
        if (leverCamera != null)
            leverCamera.enabled = false; // Kamera je isključena na startu
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

            StartCoroutine(RotateLeverWithCamera());
        }
    }

    private IEnumerator RotateLeverWithCamera()
    {
        // Uključi lever kameru
        if (leverCamera != null)
            leverCamera.enabled = true;

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

        // Kratka pauza da igrač vidi rezultat (opcionalno, npr. 0.5 sekundi)
        yield return new WaitForSeconds(0.5f);

        // Isključi lever kameru
        if (leverCamera != null)
            leverCamera.enabled = false;
    }
}
