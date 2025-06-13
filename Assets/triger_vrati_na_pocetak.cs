using UnityEngine;
using UnityEngine.SceneManagement;

public class triger_vrati_na_pocetak : MonoBehaviour
{
    public Transform pocetnaPozicija; // Pozicija na koju će se igrač vratiti
    public GameObject igrac; // Referenca na igrača

    private void OnTriggerEnter(Collider other)
    {
        // Provjeri je li objekt koji je ušao u trigger igrač
        if (other.CompareTag("Player"))
        {
            // Vrati igrača na početnu poziciju
            igrac.transform.position = pocetnaPozicija.position;

            // Opcija: resetiraj brzinu igrača ako koristi Rigidbody
            Rigidbody rb = igrac.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            Debug.Log("Igrač je resetiran na početnu poziciju!");
        }
    }
}