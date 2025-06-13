using UnityEngine;

public class let_ptica : MonoBehaviour
{
    [Header("Postavke leta")]
    public float brzina = 6f;
    public float promjenaSmjeraInterval = 4f;
    public float brzinaOkretanja = 3f;
    public float intenzitetSmutnje = 0.2f;

    private Vector3 trenutniSmjer;
    private float timer;
    private float vertikalnoOgranicenje = 0.3f;

    void Start()
    {
        PostaviPocetniSmjer();
        timer = Random.Range(0f, promjenaSmjeraInterval); // Nasumičan početni timer
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Dodaj nasumičnu smutnju za prirodnije kretanje
        DodajNasumicnuSmutnju();

        // Promjeni smjer nakon intervala
        if (timer > promjenaSmjeraInterval)
        {
            PostaviNoviSmjer();
            timer = 0f;
        }

        // Glatko okretanje prema novom smjeru
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(trenutniSmjer),
            brzinaOkretanja * Time.deltaTime
        );

        // Kretanje naprijed
        transform.position += transform.forward * brzina * Time.deltaTime;
    }

    void PostaviPocetniSmjer()
    {
        trenutniSmjer = Random.insideUnitSphere;
        trenutniSmjer.y = Mathf.Clamp(trenutniSmjer.y, -vertikalnoOgranicenje, vertikalnoOgranicenje);
        trenutniSmjer.Normalize();
    }

    void PostaviNoviSmjer()
    {
        Vector3 noviSmjer = trenutniSmjer + Random.insideUnitSphere * 0.5f;
        noviSmjer.y = Mathf.Clamp(noviSmjer.y, -vertikalnoOgranicenje, vertikalnoOgranicenje);
        trenutniSmjer = Vector3.Lerp(trenutniSmjer, noviSmjer, 0.5f).normalized;
    }

    void DodajNasumicnuSmutnju()
    {
        Vector3 smutnja = new Vector3(
            Mathf.PerlinNoise(Time.time, 0) - 0.5f,
            Mathf.PerlinNoise(0, Time.time) - 0.5f,
            Mathf.PerlinNoise(Time.time, Time.time) - 0.5f
        ) * intenzitetSmutnje;

        trenutniSmjer += smutnja;
        trenutniSmjer.Normalize();
    }
}
