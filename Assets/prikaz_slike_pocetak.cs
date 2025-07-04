using UnityEngine;
using UnityEngine.UI;

public class prikaz_slike_pocetak : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject levelImageObject; 

    [Header("Input")]
    public string useButtonName = "Use"; 

    [Header("Player Control")]
    public MonoBehaviour playerControllerScript; 

    private bool prikazujemoSliku = false;

    void Start()
    {
        
        levelImageObject.SetActive(true);
        prikazujemoSliku = true;

       
        if (playerControllerScript != null)
            playerControllerScript.enabled = false;
    }

    void Update()
    {
        if (prikazujemoSliku)
        {
            if (Input.GetButtonDown(useButtonName))
            {
                levelImageObject.SetActive(false);
                prikazujemoSliku = false;

                
                if (playerControllerScript != null)
                    playerControllerScript.enabled = true;
            }
        }
    }
}
