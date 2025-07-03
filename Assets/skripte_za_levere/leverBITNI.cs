using UnityEngine;

public class leverBITNI : MonoBehaviour
{
    [Header("Player Child Objekti")]
    public GameObject playerChild1;
    public GameObject playerChild2;

    [Header("Kamera")]
    public Camera actionCamera; // Povuci kameru koja snima spuštanje

    [Header("Objekt za pomicanje")]
    public Transform objectToMove; // Povuci objekt koji želiš pomicati

    [Header("Spuštanje objekta")]
    public float topY = 2f;      // Početna Y pozicija
    public float bottomY = 0f;   // Donja Y pozicija
    public float moveSpeed = 2f; // Brzina spuštanja

    [Header("UI Elementi")]
    public GameObject interactionText; // Povuci UI tekst (opcionalno)

    private bool playerInRange = false;
    private bool isMoving = false;
    private bool isDown = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.SetActive(false);
        if (actionCamera != null)
            actionCamera.enabled = false;

        if (objectToMove != null)
        {
            // Postavi objekt na topY na startu
            Vector3 pos = objectToMove.position;
            pos.y = topY;
            objectToMove.position = pos;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject == playerChild1 || other.gameObject == playerChild2) && !isDown)
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
        if (playerInRange && !isMoving && !isDown && Input.GetKeyDown(KeyCode.F))
        {
            if (interactionText != null)
                interactionText.SetActive(false);

            StartCoroutine(SpustiObjektRoutine());
        }
    }

    System.Collections.IEnumerator SpustiObjektRoutine()
    {
        isMoving = true;
        if (actionCamera != null)
            actionCamera.enabled = true;

        Vector3 startPos = objectToMove.position;
        Vector3 endPos = new Vector3(startPos.x, bottomY, startPos.z);
        float distance = Mathf.Abs(startPos.y - bottomY);
        float duration = distance / moveSpeed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            objectToMove.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        objectToMove.position = endPos;
        isMoving = false;
        isDown = true;

        yield return new WaitForSeconds(0.5f);

        if (actionCamera != null)
            actionCamera.enabled = false;
    }
}
