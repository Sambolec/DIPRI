using UnityEngine;
using TMPro;

public class vrata_u_krunu : MonoBehaviour
{
    [Header("Reference")]
    public GameObject player;         // Player objekt (povuci u Inspector)
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

    private bool playerUTriggeru = false;
    private bool vrataOtvorena = false;
    private bool otvaranjeUTijeku = false;

    private void Start()
    {
        if (messageText != null)
            messageText.gameObject.SetActive(false);
        if (vrata != null)
            vrata.localEulerAngles = pocetnaRotacija;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !vrataOtvorena)
        {
            playerUTriggeru = true;
            if (messageText != null)
            {
                messageText.text = tekstPoruke;
                messageText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerUTriggeru = false;
            if (messageText != null)
                messageText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerUTriggeru && !vrataOtvorena && !otvaranjeUTijeku)
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
