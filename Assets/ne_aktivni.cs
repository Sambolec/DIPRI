using UnityEngine;

public class ne_aktivni : MonoBehaviour
{
    public GameObject useButtonText;
    public GameObject pismoImage;

    void Awake()
    {
        if (useButtonText != null)
            useButtonText.SetActive(false);
        if (pismoImage != null)
            pismoImage.SetActive(false);
    }
}
