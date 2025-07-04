using UnityEngine;
using UnityEngine.SceneManagement;

public class prebaci_na_treci : MonoBehaviour
{
    public GameObject karakter1; // Povuci ovdje prvi karakter
    public GameObject karakter2; // Povuci ovdje drugi karakter

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == karakter1 || other.gameObject == karakter2)
        {
            SceneManager.LoadScene("treci_level");
        }
    }
}
