using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class krstionica_napad : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text infoText;            // UI Text (npr. "Pritisni E za interakciju")
    public Canvas infoCanvas;        // Canvas koji se prikazuje na 'Use'
    public float canvasDuration = 3f; // Koliko sekundi će canvas biti prikazan

    [Header("Enemy Spawning")]
    public GameObject enemyPrefab;   // Prefab neprijatelja
    public Transform[] spawnPoints;  // Gdje će se neprijatelji pojaviti

    [Header("Input")]
    public string useButton = "Use"; // Naziv tipke iz Input Settingsa

    private bool playerInZone = false;
    private bool canInteract = true;
    private bool enemiesSpawned = false;

    void Start()
    {
        infoText.gameObject.SetActive(false);
        infoCanvas.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canInteract)
        {
            playerInZone = true;
            infoText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            infoText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInZone && canInteract && Input.GetButtonDown(useButton))
        {
            StartCoroutine(ShowCanvasAndSpawnEnemies());
        }
    }

    IEnumerator ShowCanvasAndSpawnEnemies()
    {
        canInteract = false;
        infoText.gameObject.SetActive(false);
        infoCanvas.gameObject.SetActive(true);

        yield return new WaitForSeconds(canvasDuration);

        infoCanvas.gameObject.SetActive(false);

        if (!enemiesSpawned)
        {
            SpawnEnemies();
            enemiesSpawned = true;
        }
    }

    void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
