using UnityEngine;
using TMPro;

public class uzeo_krunu : MonoBehaviour
{
    [Header("UI Tekst")]
    public TextMeshProUGUI messageText;
    public string textZaPrikaz = "Pritisni USE za interakciju!";

    [Header("Player djeca")]
    public GameObject playerChild1;
    public GameObject playerChild2;

    [Header("Canvas i nagrada")]
    public GameObject rewardCanvas; // Povuci cijeli Canvas koji želiš prikazati
    public float canvasDisplayDuration = 3f; // Koliko sekundi će biti prikazan canvas
    public GameObject objectToSpawn;   // Prvi objekt koji se pojavljuje
    public GameObject objectToSpawn2;  // Drugi objekt koji se pojavljuje

    private bool playerInRange = false;
    private bool used = false;

    void Start()
    {
        if (messageText != null)
            messageText.gameObject.SetActive(false);
        if (rewardCanvas != null)
            rewardCanvas.SetActive(false);
        if (objectToSpawn != null)
            objectToSpawn.SetActive(false);
        if (objectToSpawn2 != null)
            objectToSpawn2.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject == playerChild1 || other.gameObject == playerChild2) && !used)
        {
            playerInRange = true;
            if (messageText != null)
            {
                messageText.text = textZaPrikaz;
                messageText.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerChild1 || other.gameObject == playerChild2)
        {
            playerInRange = false;
            if (messageText != null)
                messageText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && !used && Input.GetButtonDown("Use"))
        {
            used = true;
            if (messageText != null)
                messageText.gameObject.SetActive(false);

            StartCoroutine(ShowCanvasAndSpawnObjects());
        }
    }

    System.Collections.IEnumerator ShowCanvasAndSpawnObjects()
    {
        if (rewardCanvas != null)
            rewardCanvas.SetActive(true);

        yield return new WaitForSeconds(canvasDisplayDuration);

        if (rewardCanvas != null)
            rewardCanvas.SetActive(false);

        if (objectToSpawn != null)
            objectToSpawn.SetActive(true);
        if (objectToSpawn2 != null)
            objectToSpawn2.SetActive(true);

        gameObject.SetActive(false); // Kruna nestaje
    }
}
