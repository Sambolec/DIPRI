using UnityEngine;
using UnityEngine.SceneManagement;

public class kod_zvoneta : MonoBehaviour
{
    public Transform[] players;                 // Povuci sve player Transforms (npr. 2 djeteta)

    public string targetSceneName = "zvonke_scena"; // Naziv scene za učitavanje

    void OnTriggerEnter(Collider other)
    {
        foreach (var player in players)
        {
            // Usporedi root transform ili koristi IsChildOf za sigurnost
            if (other.transform.root == player || other.transform.IsChildOf(player))
            {
                SceneManager.LoadScene(targetSceneName);
                break;
            }
        }
    }
}
