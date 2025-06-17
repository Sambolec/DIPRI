using UnityEngine;
using UnityEngine.SceneManagement;

public class prebaci_na_treci : MonoBehaviour
{
    public GameObject playerChild1; // Povuci prvo dijete playera
    public GameObject playerChild2; // Povuci drugo dijete playera

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerChild1 || other.gameObject == playerChild2)
        {
            SceneManager.LoadScene("treci_level");
        }
    }
}
