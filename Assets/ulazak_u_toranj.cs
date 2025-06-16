using UnityEngine;
using UnityEngine.SceneManagement;

public class ulazak_u_toranj : MonoBehaviour
{
    public GameObject player; // Povuci Player objekt u Inspectoru

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            SceneManager.LoadScene("drugi_level");
        }
    }
}
