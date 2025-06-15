using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class load_u_unutrasnjost : MonoBehaviour
{
    void Start()
    {
        // Pročitaj postavke iz PlayerPrefs
        float delay = PlayerPrefs.GetFloat("LoadingDelay", 3f);
        string targetScene = PlayerPrefs.GetString("TargetScene", "unutrasnjost_crkve");

        StartCoroutine(LoadSceneAfterDelay(delay, targetScene));
    }

    IEnumerator LoadSceneAfterDelay(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
