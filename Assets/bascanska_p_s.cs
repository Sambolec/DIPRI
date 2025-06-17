using UnityEngine;
using System.Collections;

public class bascanska_p_s : MonoBehaviour
{
    [Header("Reference")]
    public GameObject playerChild1;     // Povuci prvo dijete playera
    public GameObject playerChild2;     // Povuci drugo dijete playera
    public GameObject objektZaSpustiti; // Objekt koji će se spuštati
    public GameObject interactionText;  // UI tekst za interakciju

    [Header("Postavke spuštanja")]
    public float targetY = -2.5f;       // Točna Y pozicija na koju se spušta
    public float moveSpeed = 2f;        // Brzina spuštanja

    private bool playerInRange = false;
    private bool isMoving = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerChild1 || other.gameObject == playerChild2)
        {
            playerInRange = true;
            if (!isMoving && interactionText != null)
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
        if (playerInRange && !isMoving && Input.GetButtonDown("Use"))
        {
            if (interactionText != null)
                interactionText.SetActive(false);

            if (objektZaSpustiti != null)
                StartCoroutine(SpustiObjektNaY());
        }
    }

    private IEnumerator SpustiObjektNaY()
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
