using UnityEngine;
using UnityEngine.SceneManagement;

public class u_krunu : MonoBehaviour
{
    public string imeScene = "scena_kruna"; // Naziv scene koju želiš učitati

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Provjerava je li ušao objekt s tagom "Player"
        {
            SceneManager.LoadScene(imeScene);
        }
    }
}
