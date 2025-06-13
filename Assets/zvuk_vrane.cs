using UnityEngine;

public class zvuk_vrane : MonoBehaviour
{
    public AudioSource audioSource;
    public float minDelay = 3f; // minimalno vrijeme između zvukova
    public float maxDelay = 8f; // maksimalno vrijeme između zvukova

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundWithRandomDelay());
    }

    System.Collections.IEnumerator PlaySoundWithRandomDelay()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
            audioSource.Play();
        }
    }
}
