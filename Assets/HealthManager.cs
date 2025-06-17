using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    public int maxHealth = 100;

    [SerializeField]
    private int currentHealth;
    public int CurrentHealth => currentHealth;

    public UnityEvent<int> OnHealthChanged;
    public UnityEvent OnDeath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            currentHealth = maxHealth;

            if (OnHealthChanged == null)
                OnHealthChanged = new UnityEvent<int>();
            if (OnDeath == null)
                OnDeath = new UnityEvent();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
            OnDeath?.Invoke();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }
}
