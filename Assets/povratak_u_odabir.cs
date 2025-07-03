using UnityEngine;
using UnityEngine.SceneManagement;

public class povratak_u_odabir : MonoBehaviour
{
    [Header("Player")]
    public GameObject playerChild1;
    public GameObject playerChild2;

    [Header("Ime scene za učitavanje")]
    public string sceneToLoad = "odabir_ulaza";

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerChild1 || other.gameObject == playerChild2)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
