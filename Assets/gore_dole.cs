using UnityEngine;
using System.Collections;

public class gore_dole : MonoBehaviour
{
    public float topY = 5f;    // Y-koordinata gdje strop ide gore
    public float bottomY = 1f; // Y-koordinata gdje strop ide dolje
    public float moveSpeed = 2f;
    public float stayUpDuration = 2f;
    public float stayDownDuration = 2f;

    private bool movingUp = true;

    void Start()
    {
        StartCoroutine(MoveCeiling());
    }

    IEnumerator MoveCeiling()
    {
        while (true)
        {
            if (movingUp)
            {
                // Pomicanje prema gore
                while (Mathf.Abs(transform.position.y - topY) > 0.01f)
                {
                    Vector3 newPos = new Vector3(
                        transform.position.x,
                        Mathf.MoveTowards(transform.position.y, topY, moveSpeed * Time.deltaTime),
                        transform.position.z
                    );
                    transform.position = newPos;
                    yield return null;
                }
                yield return new WaitForSeconds(stayUpDuration);
                movingUp = false;
            }
            else
            {
                // Pomicanje prema dolje
                while (Mathf.Abs(transform.position.y - bottomY) > 0.01f)
                {
                    Vector3 newPos = new Vector3(
                        transform.position.x,
                        Mathf.MoveTowards(transform.position.y, bottomY, moveSpeed * Time.deltaTime),
                        transform.position.z
                    );
                    transform.position = newPos;
                    yield return null;
                }
                yield return new WaitForSeconds(stayDownDuration);
                movingUp = true;
            }
        }
    }
}
