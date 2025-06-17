using UnityEngine;
using System.Collections;

public class treci_otvaranje_vrata : MonoBehaviour
{
    [Header("Child Objekti Playera")]
    public GameObject playerChild1;    // Povuci prvo dijete playera
    public GameObject playerChild2;    // Povuci drugo dijete playera

    [Header("Postavke Vrata")]
    public GameObject door;             // Povuci vrata koja se otvaraju
    public float targetZRotation = 90f; // Konačna rotacija u stupnjevima
    public float rotationSpeed = 2f;    // Brzina rotacije

    private bool doorOpened = false;
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = door.transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        // Provjeri je li sudar s jednim od child objekata
        if ((other.gameObject == playerChild1 || other.gameObject == playerChild2) && !doorOpened)
        {
            StartCoroutine(RotateDoor());
        }
    }

    IEnumerator RotateDoor()
    {
        doorOpened = true;
        float elapsed = 0f;
        Quaternion startRotation = door.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(
            initialRotation.eulerAngles.x,
            initialRotation.eulerAngles.y,
            initialRotation.eulerAngles.z + targetZRotation
        );

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * rotationSpeed;
            door.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsed);
            yield return null;
        }

        // Osiguraj točnu konačnu rotaciju
        door.transform.rotation = targetRotation;
    }
}
