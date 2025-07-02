using UnityEngine;
using TMPro; 

public class uzeo_krunu : MonoBehaviour
{
    [Header("UI Tekst")]
    public TextMeshProUGUI messageText; 
    public string textZaPrikaz = "Pritisni USE za interakciju!";

    [Header("Player djeca")]
    public GameObject playerChild1; 
    public GameObject playerChild2; 

    private bool playerInRange = false;

    void Start()
    {
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerChild1 || other.gameObject == playerChild2)
        {
            playerInRange = true;
            if (messageText != null)
            {
                messageText.text = textZaPrikaz;
                messageText.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerChild1 || other.gameObject == playerChild2)
        {
            playerInRange = false;
            if (messageText != null)
                messageText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetButtonDown("Use"))
        {
            if (messageText != null)
                messageText.gameObject.SetActive(false);

            gameObject.SetActive(false); // Sakrij trigger objekt
        }
    }
}
