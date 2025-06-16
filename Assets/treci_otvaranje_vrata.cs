using UnityEngine;
using System.Collections;

public class treci_otvaranje_vrata : MonoBehaviour
{
    [Header("Postavke")]
    public GameObject player;        // Povuci Player objekt
    public GameObject door;          // Povuci vrata koja se otvaraju
    public float targetZRotation = 90f; // Konačna rotacija u stupnjevima
    public float rotationSpeed = 2f; // Brzina rotacije (veća vrijednost = brže)

    private bool doorOpened = false;
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = door.transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !doorOpened)
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
