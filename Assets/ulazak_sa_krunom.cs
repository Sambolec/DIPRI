using UnityEngine;
using TMPro;
using System.Collections;

public class ulazak_sa_krunom : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text infoText1; // Povuci prvi TMP tekst iz canvasa
    public TMP_Text infoText2; // Povuci drugi TMP tekst iz canvasa

    [Header("Vrata")]
    public Transform vrataTransform; // Povuci vrata (transform)
    public Vector3 pocetnaRotacija;  // Početna Euler rotacija vrata
    public Vector3 zavrsnaRotacija;  // Završna Euler rotacija vrata
    public float trajanjeOtvaranja = 1.5f;

    [Header("Players")]
    public Transform[] players = new Transform[2]; // Povuci svoja 2 playera (childa ili bilo gdje u sceni)

    private int stanje = 0; // 0 - ništa, 1 - prvi tekst, 2 - drugi tekst, 3 - otvaranje vrata
    private bool vrataOtvorena = false;

    void Start()
    {
        if (infoText1 != null) infoText1.gameObject.SetActive(false);
        if (infoText2 != null) infoText2.gameObject.SetActive(false);
        if (vrataTransform != null)
            vrataTransform.localEulerAngles = pocetnaRotacija;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            stanje++;
            if (stanje == 1)
            {
                if (infoText1 != null) infoText1.gameObject.SetActive(true);
                if (infoText2 != null) infoText2.gameObject.SetActive(false);
            }
            else if (stanje == 2)
            {
                if (infoText1 != null) infoText1.gameObject.SetActive(false);
                if (infoText2 != null) infoText2.gameObject.SetActive(true);
            }
            else if (stanje == 3 && !vrataOtvorena)
            {
                if (infoText1 != null) infoText1.gameObject.SetActive(false);
                if (infoText2 != null) infoText2.gameObject.SetActive(false);
                StartCoroutine(OtvoriVrata());
            }
        }
    }

    IEnumerator OtvoriVrata()
    {
        vrataOtvorena = true;
        Quaternion pocetak = Quaternion.Euler(pocetnaRotacija);
        Quaternion kraj = Quaternion.Euler(zavrsnaRotacija);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / trajanjeOtvaranja;
            vrataTransform.localRotation = Quaternion.Lerp(pocetak, kraj, t);
            yield return null;
        }
        vrataTransform.localRotation = kraj;
    }
}
