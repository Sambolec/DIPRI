using UnityEngine;
using UnityEngine.UI;

public class prikazivanje_pr_pisma : MonoBehaviour
{
    public GameObject tekstObjekt;   // UI Text ili TMP_Text GameObject
    public GameObject slikaObjekt;   // UI Image GameObject
    public GameObject playerObjekt;  // Player GameObject

    private void Start()
    {
        if (tekstObjekt != null)
            tekstObjekt.SetActive(false);
        if (slikaObjekt != null)
            slikaObjekt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObjekt)
        {
            if (tekstObjekt != null)
                tekstObjekt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerObjekt)
        {
            if (tekstObjekt != null)
                tekstObjekt.SetActive(false);
            if (slikaObjekt != null)
                slikaObjekt.SetActive(false);
        }
    }

    // Poveži ovu metodu na USE button kroz Inspector
    public void PrikaziSliku()
    {
        if (slikaObjekt != null)
            slikaObjekt.SetActive(true);
    }
}
