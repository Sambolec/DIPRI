using UnityEngine;

public class BossWeaponDamage : MonoBehaviour
{
    public int damage = 30;
    public string playerTag = "Player";
    private bool canDealDamage = false;

    // Call this to enable damage (from animation event or code)
    public void EnableDamage() => canDealDamage = true;
    public void DisableDamage() => canDealDamage = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!canDealDamage) return;
        if (other.CompareTag(playerTag))
        {
            var health = other.GetComponent<CharacterHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
