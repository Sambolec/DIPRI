using UnityEngine;
using UnityEngine.SceneManagement;

public class triger_u_unutrasnjost : MonoBehaviour
{
    [Header("Postavke")]
    public GameObject interactionText; // Povuci Text objekt iz Canvasa
    public GameObject player; // Povuci Player objekt iz Hierarchy
    public float loadingDelay = 3f; // Vrijeme čekanja u sekundama

    private bool playerInRange = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (interactionText != null)
                interactionText.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            if (interactionText != null)
                interactionText.SetActive(false);
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetButtonDown("Use"))
        {
            // Spremi podatke za loading screen
            PlayerPrefs.SetFloat("LoadingDelay", loadingDelay);
            PlayerPrefs.SetString("TargetScene", "unutrasnjost_crkve");

            // Učitaj loading scenu
            SceneManager.LoadScene("loadingBar/Scenes/scene0");
        }
    }
}
