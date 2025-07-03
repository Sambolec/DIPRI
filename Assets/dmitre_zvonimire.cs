using UnityEngine;
using TMPro;

public class dmitre_zvonimire : MonoBehaviour
{
    [Header("Players")]
    public Transform[] players; // Povuci sve player Transforme (npr. 2 djeteta ili više)

    [Header("UI")]
    public TMP_Text infoText;   // Povuci TMP tekst iz scene
    public Canvas infoCanvas;   // Povuci Canvas iz scene
    public string useButton = "Use"; // Naziv tipke iz Input Settingsa

    private bool playerInZone = false;
    private bool canvasActive = false;
    private bool textActive = false;

    // Pamti koliko je playera trenutno u triggeru (radi s više igrača!)
    private int playersInTrigger = 0;

    void Start()
    {
        if (infoText != null) infoText.gameObject.SetActive(false);
        if (infoCanvas != null) infoCanvas.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other.transform))
        {
            playersInTrigger++;
            playerInZone = true;
            if (infoText != null && !canvasActive)
            {
                infoText.gameObject.SetActive(true);
                textActive = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other.transform))
        {
            playersInTrigger = Mathf.Max(0, playersInTrigger - 1);
            if (playersInTrigger == 0)
            {
                playerInZone = false;
                if (infoText != null) infoText.gameObject.SetActive(false);
                if (infoCanvas != null) infoCanvas.gameObject.SetActive(false);
                canvasActive = false;
                textActive = false;
            }
        }
    }

    void Update()
    {
        if (playerInZone && Input.GetButtonDown(useButton))
        {
            if (textActive)
            {
                // Sakrij tekst, prikaži canvas
                if (infoText != null) infoText.gameObject.SetActive(false);
                if (infoCanvas != null) infoCanvas.gameObject.SetActive(true);
                textActive = false;
                canvasActive = true;
            }
            else if (canvasActive)
            {
                // Sakrij canvas
                if (infoCanvas != null) infoCanvas.gameObject.SetActive(false);
                canvasActive = false;
            }
        }
    }

    // Provjerava je li neki Transform ili njegov roditelj u arrayu players
    private bool IsPlayer(Transform t)
    {
        foreach (var player in players)
        {
            if (t == player || t.IsChildOf(player))
                return true;
        }
        return false;
    }
}
