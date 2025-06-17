using UnityEngine;
using UnityEngine.SceneManagement;

public class ulazak_u_toranj : MonoBehaviour
{
    public GameObject player1; // Drag the first player object here in the Inspector
    public GameObject player2; // Drag the second player object here in the Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player1 || other.gameObject == player2)
        {
            SceneManager.LoadScene("drugi_level");
        }
    }
}
