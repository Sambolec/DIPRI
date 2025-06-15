using UnityEngine;
using UnityEngine.SceneManagement;

public class rupa_za_1 : MonoBehaviour
{
    public GameObject player; // Povuci Player objekt u Inspectoru

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            SceneManager.LoadScene("prvi_lvl");
        }
    }
}
