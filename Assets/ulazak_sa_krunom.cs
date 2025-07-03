using UnityEngine;
using TMPro;

public class ulazak_sa_krunom : MonoBehaviour
{
    [Header("Player References")]
    public GameObject playerChild1;
    public GameObject playerChild2;

    [Header("UI References")]
    public Canvas infoCanvas;              // Cijeli canvas koji se prikazuje prvi
    public TextMeshProUGUI infoText;       // Tekst koji se prikazuje kasnije

    [Header("Tekst")]
    public string drugiTekst = "Stisni U za otvaranje vrata";

    [Header("Door Settings")]
    public Transform doorTransform;
    public Vector3 startRotation;
    public Vector3 endRotation;
    public float openSpeed = 2f;

    private bool playerInZone = false;
    private int interactionStep = 0;
    private bool doorOpened = false;

    void Start()
    {
        if (infoCanvas != null)
            infoCanvas.gameObject.SetActive(false);

        if (infoText != null)
            infoText.gameObject.SetActive(false);

        if (doorTransform != null)
            doorTransform.localEulerAngles = startRotation;
    }

    void Update()
    {
        if (playerInZone && !doorOpened)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                if (interactionStep == 0)
                {
                    // Prvi klik: makni canvas
                    if (infoCanvas != null)
                        infoCanvas.gameObject.SetActive(false);
                    interactionStep = 1;
                }
                else if (interactionStep == 1)
                {
                    // Drugi klik: prikaži tekst
                    if (infoText != null)
                    {
                        infoText.text = drugiTekst;
                        infoText.gameObject.SetActive(true);
                    }
                    interactionStep = 2;
                }
                else if (interactionStep == 2)
                {
                    // Treći klik: makni tekst i otvori vrata
                    if (infoText != null)
                        infoText.gameObject.SetActive(false);
                    StartCoroutine(OpenDoor());
                    interactionStep = 3;
                }
            }
        }
    }

    System.Collections.IEnumerator OpenDoor()
    {
        Quaternion startRot = Quaternion.Euler(startRotation);
        Quaternion endRot = Quaternion.Euler(endRotation);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            doorTransform.localRotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
        doorTransform.localRotation = endRot;
        doorOpened = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerChild1 || other.gameObject == playerChild2)
        {
            playerInZone = true;
            if (!doorOpened)
            {
                if (infoCanvas != null)
                    infoCanvas.gameObject.SetActive(true);
                if (infoText != null)
                    infoText.gameObject.SetActive(false);
                interactionStep = 0;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerChild1 || other.gameObject == playerChild2)
        {
            playerInZone = false;
            if (infoCanvas != null)
                infoCanvas.gameObject.SetActive(false);
            if (infoText != null)
                infoText.gameObject.SetActive(false);
        }
    }
}
