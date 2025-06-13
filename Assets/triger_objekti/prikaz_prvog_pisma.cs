using UnityEngine;

public class prikaz_prvog_pisma : MonoBehaviour
{
    public GameObject useButtonText;
    public GameObject pismoImage;

    private bool playerInRange = false;
    private bool imageVisible = false;

    void Awake() // Koristimo Awake umesto Start da se izvrši PRE svega drugog
    {
       
        useButtonText.SetActive(false);
        pismoImage.SetActive(false);
    }

    void Update()
    {
        // Provjeri je li igrač u području i da li pritišće USE tipku
        if (playerInRange && Input.GetButtonDown("Use"))
        {
            Debug.Log("USE pritisnut dok je igrač u području");
            if (!imageVisible)
            {
                ShowImage();
            }
            else
            {
                HideImage();
            }
        }
    }

    void ShowImage()
    {
        Debug.Log("Pokušavam prikazati sliku");
        if (useButtonText != null) useButtonText.SetActive(false);
        if (pismoImage != null)
        {
            pismoImage.SetActive(true);
            Debug.Log("Slika aktivirana");
        }
        imageVisible = true;
    }

    void HideImage()
    {
        Debug.Log("Pokušavam sakriti sliku");
        if (pismoImage != null)
        {
            pismoImage.SetActive(false);
            Debug.Log("Slika deaktivirana");
        }
        if (!playerInRange) return; // Ako igrač nije u području, ne prikazuj tekst
        if (useButtonText != null) useButtonText.SetActive(true);
        imageVisible = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Igrač ušao u trigger područje");
            playerInRange = true;
            if (!imageVisible && useButtonText != null)
            {
                useButtonText.SetActive(true);
                Debug.Log("Use Button Text aktiviran");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Igrač izašao iz trigger područja");
            playerInRange = false;
            if (useButtonText != null) useButtonText.SetActive(false);
            if (pismoImage != null) pismoImage.SetActive(false);
            imageVisible = false;
        }
    }
}
