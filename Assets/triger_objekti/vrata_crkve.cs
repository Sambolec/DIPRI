using UnityEngine;
using UnityEngine.SceneManagement;

public class vrata_crkve : MonoBehaviour
{

    public GameObject enterText;
    public string unutrasnjost_crkve;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        enterText.SetActive(false);


    }

    // Update is called once per frame
    private void OnTriggerStay(Collider Igrac)
    {
        if (Igrac.gameObject.tag == "Player")
        {
            enterText.SetActive(true);
            if (Input.GetButtonDown("Use"))
            {
                spawn.enterPoint = gameObject.name;
                SceneManager.LoadScene(unutrasnjost_crkve);
            }


        }

    }
    private void OnTriggerExit(Collider Igrac)
    {
        if (Igrac.gameObject.tag == "Player")
        {
            enterText.SetActive(false);
        }
    }
}
