using UnityEngine;
public class AutoDestroy : MonoBehaviour
{
    public float lifetime = 0.08f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
