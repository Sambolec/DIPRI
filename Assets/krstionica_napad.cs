using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class krstionica_napad : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text infoText;
    public Canvas infoCanvas;
    public float canvasDuration = 3f;

    [Header("Input")]
    public string useButton = "Use";

    [Header("Aktiviraj ove objekte nakon 3 klika")]
    public GameObject objekt1;   // Povuci ovdje prvi objekt (u sceni, isključen)
    public GameObject objekt2;   // Povuci ovdje drugi objekt (u sceni, isključen)

    private bool playerInZone = false;
    private bool canInteract = true;
    private int uranjanja = 0;
    private bool objektiAktivirani = false;

    void Start()
    {
        infoText.gameObject.SetActive(false);
        infoCanvas.gameObject.SetActive(false);

        // Po želji, možeš ih ovdje automatski isključiti na početku:
        // if (objekt1 != null) objekt1.SetActive(false);
        // if (objekt2 != null) objekt2.SetActive(false);
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
        if (playerInZone && canInteract && Input.GetButtonDown(useButton) && !objektiAktivirani)
        {
            StartCoroutine(ShowCanvasAndCountUrone());
        }
    }

    IEnumerator ShowCanvasAndCountUrone()
    {
        canInteract = false;
        infoText.gameObject.SetActive(false);
        infoCanvas.gameObject.SetActive(true);

        yield return new WaitForSeconds(canvasDuration);

        infoCanvas.gameObject.SetActive(false);

        uranjanja++;

        if (uranjanja >= 3 && !objektiAktivirani)
        {
            AktivirajObjekte();
            objektiAktivirani = true;
        }
        else
        {
            canInteract = true;
        }
    }

    void AktivirajObjekte()
    {
        if (objekt1 != null) objekt1.SetActive(true);
        if (objekt2 != null) objekt2.SetActive(true);
    }
}
