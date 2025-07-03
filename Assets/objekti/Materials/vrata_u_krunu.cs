using UnityEngine;
using TMPro;

public class vrata_u_krunu : MonoBehaviour
{
    [Header("Reference")]
    public GameObject[] players;      // Povuci sve player objekte u Inspector
    public TMP_Text messageText;      // TMP_Text za poruku (povuci iz Canvasa)
    public Transform vrata;           // Transform vrata (povuci vrata objekt)

    [Header("Rotacija vrata")]
    public Vector3 pocetnaRotacija;   // Početna rotacija vrata (Euler angles)
    public Vector3 zavrsnaRotacija;   // Završna rotacija vrata (Euler angles)
    public float brzinaOtvaranja = 2f; // Brzina otvaranja vrata

    [Header("Poruka")]
    [TextArea]
    public string tekstPoruke = "Pritisni USE za otvaranje vrata";

    [Header("Input")]
    public string useInputName = "Use"; // Naziv tipke iz Input Managera

    private bool vrataOtvorena = false;
    private bool otvaranjeUTijeku = false;
    private GameObject playerUTriggeru = null;

    private void Start()
    {
        if (messageText != null)
            messageText.gameObject.SetActive(false);
        if (vrata != null)
            vrata.localEulerAngles = pocetnaRotacija;
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var p in players)
        {
            if (other.gameObject == p && !vrataOtvorena)
            {
                playerUTriggeru = p;
                if (messageText != null)
                {
                    messageText.text = tekstPoruke;
                    messageText.gameObject.SetActive(true);
                }
                break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var p in players)
        {
            if (other.gameObject == p)
            {
                if (playerUTriggeru == p)
                    playerUTriggeru = null;
                if (messageText != null)
                    messageText.gameObject.SetActive(false);
                break;
            }
        }
    }

    private void Update()
    {
        if (playerUTriggeru != null && !vrataOtvorena && !otvaranjeUTijeku)
        {
            if (Input.GetButtonDown(useInputName))
            {
                StartCoroutine(OtvoriVrata());
                if (messageText != null)
                    messageText.gameObject.SetActive(false);
            }
        }
    }

    private System.Collections.IEnumerator OtvoriVrata()
    {
        otvaranjeUTijeku = true;
        Quaternion pocetna = Quaternion.Euler(pocetnaRotacija);
        Quaternion zavrsna = Quaternion.Euler(zavrsnaRotacija);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * brzinaOtvaranja;
            if (vrata != null)
                vrata.localRotation = Quaternion.Slerp(pocetna, zavrsna, t);
            yield return null;
        }

        if (vrata != null)
            vrata.localRotation = zavrsna;
        vrataOtvorena = true;
        otvaranjeUTijeku = false;
    }
}
