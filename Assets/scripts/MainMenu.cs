using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;    
    public GameObject controlsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("ispred_crkve"); // Zamijeni s točnim imenom tvoje scene
    }

    public void ShowControls()
    {
        mainMenuPanel.SetActive(false);       // Sakrij glavni meni
        controlsPanel.SetActive(true);        // Prikaži kontrole
    }

    public void HideControls()
    {
        controlsPanel.SetActive(false);       // Sakrij kontrole
        mainMenuPanel.SetActive(true);        // Vrati glavni meni
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit(); // Radi samo u buildanoj igri (ne unutar Editora)
    }
}
