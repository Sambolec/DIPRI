using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class prelazak_na_prvi_lvl : MonoBehaviour
{
    public string scenaZaUcitati = "prvi_lvl";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(scenaZaUcitati);
        }
    }
}
   