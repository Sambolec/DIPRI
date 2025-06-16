using UnityEngine;

public class bascanska_p_s : MonoBehaviour
{
    [Header("Reference")]
    public GameObject player;           // Povuci Player objekt
    public GameObject objektZaSpustiti; // Objekt koji će se spuštati
    public GameObject interactionText;  // UI tekst za interakciju

    [Header("Postavke spuštanja")]
    public float targetY = -2.5f;       // Točna Y pozicija na koju se spušta
    public float moveSpeed = 2f;        // Brzina spuštanja

    private bool playerInRange = false;
    private bool isMoving = false;

    void Start()
    {
        // Sakrij tekst na početku
        if (interactionText != null)
            interactionText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
            if (!isMoving && interactionText != null)
                interactionText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            if (interactionText != null)
                interactionText.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && !isMoving && Input.GetButtonDown("Use"))
        {
            if (interactionText != null)
                interactionText.SetActive(false);

            if (objektZaSpustiti != null)
                StartCoroutine(SpustiObjektNaY());
        }
    }

    private System.Collections.IEnumerator SpustiObjektNaY()
    {
        isMoving = true;
        Vector3 startPos = objektZaSpustiti.transform.position;
        Vector3 targetPos = new Vector3(startPos.x, targetY, startPos.z);
        float distance = Mathf.Abs(startPos.y - targetY);
        float trajanje = distance / moveSpeed;
        float elapsed = 0f;

        while (elapsed < trajanje)
        {
            elapsed += Time.deltaTime;
            objektZaSpustiti.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / trajanje);
            yield return null;
        }
        objektZaSpustiti.transform.position = targetPos;
        isMoving = false;
    }
}
