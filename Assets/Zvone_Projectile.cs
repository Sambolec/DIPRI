using UnityEngine;

public class Zvone_Projectile : MonoBehaviour
{
    public int damage = 25;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Only damage the player (CharacterHealth)
        var charHealth = collision.gameObject.GetComponent<CharacterHealth>();
        if (charHealth != null)
        {
            charHealth.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // (Optional) Add more checks for other player health scripts if needed

        Destroy(gameObject); // Destroy projectile on any collision
    }
}
