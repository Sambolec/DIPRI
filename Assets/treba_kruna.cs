using UnityEngine;
using TMPro;

public class treba_kruna : MonoBehaviour
{
    public GameObject player;
    public TMP_Text messageText;

    private void Start()
    {
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && other.CompareTag("Player"))
        {
            if (messageText != null)
                messageText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && other.CompareTag("Player"))
        {
            if (messageText != null)
                messageText.gameObject.SetActive(false);
        }
    }
}
