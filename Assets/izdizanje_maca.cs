using UnityEngine;

public class izdizanje_maca : MonoBehaviour
{
    [Header("Player Detection")]
    public GameObject player;              // Povuci Player objekt

    [Header("Mac Movement")]
    public GameObject sword;               // Povuci Mac objekt
    public float swordMoveAmount = 0.3f;   // Za koliko se mac pomakne po kliku (Y osi)

    [Header("Click Settings")]
    public int requiredClicks = 5;         // Koliko puta treba kliknuti Use button

    [Header("UI Elements")]
    public GameObject interactionText;     // Povuci UI tekst iz Canvasa (npr. "Pritisni E")
    public GameObject rewardCanvas;        // Povuci Canvas koji se prikazuje na kraju
    public float canvasDisplayDuration = 3f; // Koliko sekundi canvas ostaje prikazan

    // Privatne varijable
    private bool playerInRange = false;
    private int currentClicks = 0;
    private bool completed = false;

    void Start()
    {
        // Sakrij sve na početku
        if (interactionText != null) interactionText.SetActive(false);
        if (rewardCanvas != null) rewardCanvas.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !completed)
        {
            playerInRange = true;
            // Prikaži tekst kad igrač uđe u trigger
            if (interactionText != null) interactionText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            // Sakrij tekst kad igrač izađe
            if (interactionText != null) interactionText.SetActive(false);
            // Ako nije završeno, resetiraj
            if (!completed)
            {
                currentClicks = 0;
            }
        }
    }

    void Update()
    {
        if (playerInRange && !completed && Input.GetButtonDown("Use"))
        {
            currentClicks++;

            // Pomakni mač gore po Y osi
            if (sword != null)
            {
                Vector3 newPosition = sword.transform.position;
                newPosition.y += swordMoveAmount;
                sword.transform.position = newPosition;
            }

            // Provjeri je li dosegnut potreban broj klikova
            if (currentClicks >= requiredClicks)
            {
                completed = true;
                // Sakrij tekst i prikaži canvas
                if (interactionText != null) interactionText.SetActive(false);
                if (rewardCanvas != null) rewardCanvas.SetActive(true);

                // Pokreni korutinu za skrivanje canvasa nakon određenog vremena
                if (rewardCanvas != null)
                {
                    StartCoroutine(HideCanvasAfterDelay(canvasDisplayDuration));
                }
            }
        }
    }

    private System.Collections.IEnumerator HideCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (rewardCanvas != null)
        {
            rewardCanvas.SetActive(false);
        }
    }
}
