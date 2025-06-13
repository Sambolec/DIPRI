using UnityEngine;
using UnityEngine.UI;

public class uzimanje_maca : MonoBehaviour
{
    [Header("Reference")]
    public GameObject interactionTextUI;
    public Transform player;
    public float interactionDistance = 2f;

    [Header("Sword Settings")]
    public int requiredPresses = 10;
    public int pressesToDisappear = 5; // Nakon koliko pritisaka mač nestaje
    public float totalMovementDistance = 1f; // Ukupna udaljenost podizanja

    private bool playerInRange = false;
    private int pressCount = 0;
    private Vector3 initialPosition;
    private bool isExtracting = false;
    private Renderer swordRenderer;

    void Start()
    {
        initialPosition = transform.position;
        swordRenderer = GetComponent<Renderer>();

        if (interactionTextUI == null)
        {
            Debug.LogError("Interaction Text UI nije postavljen!");
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Početno sakrij tekst
        if (interactionTextUI != null)
            interactionTextUI.SetActive(false);
    }

    void Update()
    {
        // Provjeri je li igrač u dometu
        CheckPlayerDistance();

        // Ako je igrač u dometu, prikaži tekst i omogući interakciju
        if (playerInRange)
        {
            if (interactionTextUI != null && !isExtracting)
                interactionTextUI.SetActive(true);

            // Ako igrač pritisne USE tipku
            if (Input.GetButtonDown("Use"))
            {
                pressCount++;
                Debug.Log("Pritisak: " + pressCount + "/" + requiredPresses);

                if (!isExtracting && pressCount >= 1)
                {
                    isExtracting = true;
                }

                // Pomakni mač prema gore
                if (isExtracting)
                {
                    // Izračunaj novu poziciju mača
                    float extractionProgress = (float)pressCount / requiredPresses;
                    Vector3 newPosition = initialPosition + new Vector3(0, totalMovementDistance * extractionProgress, 0);
                    transform.position = newPosition;

                    Debug.Log("Mač se podiže: " + extractionProgress * 100 + "%");

                    // Ako je dosegnut broj pritisaka za nestajanje
                    if (pressCount >= pressesToDisappear)
                    {
                        // Sakrij mač
                        if (swordRenderer != null)
                            swordRenderer.enabled = false;

                        // Sakrij tekst
                        if (interactionTextUI != null)
                            interactionTextUI.SetActive(false);

                        Debug.Log("Mač je nestao nakon " + pressCount + " pritisaka!");

                        // Opcija: Uništi objekt nakon nekoliko sekundi
                        Destroy(gameObject, 2f);
                    }
                }
            }
        }
        else
        {
            if (interactionTextUI != null)
                interactionTextUI.SetActive(false);
        }
    }

    void CheckPlayerDistance()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            playerInRange = distance <= interactionDistance;
        }
    }
}
