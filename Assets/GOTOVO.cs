using UnityEngine;

public class GOTOVO : MonoBehaviour
{
    [Header("Players")]
    public Transform[] characters;     // Povuci svoja 2 (ili više) karaktera ovdje

    [Header("UI")]
    public Canvas infoCanvas;          // Povuci svoj Canvas ovdje
    public string useButton = "Use";   // Naziv tipke iz Input Managera

    private bool canvasActive = false;

    void Start()
    {
        if (infoCanvas != null)
            infoCanvas.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsMyCharacter(other.transform))
        {
            if (infoCanvas != null)
                infoCanvas.gameObject.SetActive(true);

            canvasActive = true;
        }
    }

    void Update()
    {
        if (canvasActive && Input.GetButtonDown(useButton))
        {
            if (infoCanvas != null)
                infoCanvas.gameObject.SetActive(false);

            canvasActive = false;
        }
    }

    // Provjerava je li objekt jedan od tvojih karaktera ili njihov child
    private bool IsMyCharacter(Transform t)
    {
        foreach (var character in characters)
        {
            if (t == character || t.IsChildOf(character))
                return true;
        }
        return false;
    }
}
