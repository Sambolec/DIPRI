using UnityEngine;

public class Kretnja : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal"); // A, D
        float moveV = Input.GetAxis("Vertical");   // W, S

        Vector3 movement = new Vector3(moveH, 0, moveV);
        rb.AddForce(movement * speed);
    }
}