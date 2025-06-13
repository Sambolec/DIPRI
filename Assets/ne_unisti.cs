using UnityEngine;
using UnityEngine.SceneManagement;

public class ne_unisti : MonoBehaviour
{
    private static ne_unisti instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Spremi igrača kroz scene
        }
        else
        {
            Destroy(gameObject); // Uništi duplikate ako se pojave
        }
    }
}