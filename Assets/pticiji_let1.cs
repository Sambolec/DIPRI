using UnityEngine;

public class pticiji_let1 : MonoBehaviour
{
    public float minSpeed = 2f;
    public float maxSpeed = 5f;
    public float changeDirectionInterval = 2f; // koliko često ptica mijenja smjer

    private Vector3 direction;
    private float speed;
    private float timer;

    void Start()
    {
        SetRandomDirectionAndSpeed();
        timer = changeDirectionInterval * Random.Range(0.5f, 1.5f); // svaka ptica drugačije mijenja smjer
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SetRandomDirectionAndSpeed();
            timer = changeDirectionInterval * Random.Range(0.5f, 1.5f);
        }
    }

    void SetRandomDirectionAndSpeed()
    {
        // Nasumičan smjer u 2D (X,Z) ili 3D prostoru
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.2f), Random.Range(-1f, 1f)).normalized;
        speed = Random.Range(minSpeed, maxSpeed);
        // Opcionalno: rotiraj pticu prema smjeru kretanja
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }
}
