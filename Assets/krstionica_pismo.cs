using UnityEngine;
using TMPro;

public class SimpleTriggerTextUI : MonoBehaviour
{
    public Canvas infoCanvas;
    public TMP_Text infoText;         // Povuci TMP tekst ovdje
    [TextArea]
    public string poruka = "Pritisni E za interakciju"; // Tekst koji želiš prikazati
    public string useButton = "Use";  // Naziv tipke iz Input Settingsa

    private bool playerInZone = false;
    private bool canvasActive = false;

    void Start()
    {
        infoCanvas.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            infoText.text = poruka;
            infoCanvas.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            infoCanvas.gameObject.SetActive(false);
            canvasActive = false;
        }
    }

    void Update()
    {
        if (playerInZone && Input.GetButtonDown(useButton))
        {
            canvasActive = !canvasActive;
            infoCanvas.gameObject.SetActive(canvasActive);
        }
    }
}
