using UnityEngine;
using UnityEngine.SceneManagement;

public class rupa_za_1 : MonoBehaviour
{
    public GameObject playerChild1; // Povuci prvo dijete playera u Inspectoru
    public GameObject playerChild2; // Povuci drugo dijete playera u Inspectoru

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerChild1 || other.gameObject == playerChild2)
        {
            SceneManager.LoadScene("prvi_lvl");
        }
    }
}
