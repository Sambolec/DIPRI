using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    public int maxHealth = 100;
    public int CurrentHealth { get; private set; }

    public UnityEvent<int> OnHealthChanged;
    public UnityEvent OnDeath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CurrentHealth = maxHealth;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        if (CurrentHealth <= 0) return;
        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth);
        if (CurrentHealth <= 0)
            OnDeath?.Invoke();
    }

    public void ResetHealth()
    {
        CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth);
    }
}
