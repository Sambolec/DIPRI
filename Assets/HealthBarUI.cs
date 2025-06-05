using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        // Subscribe to HealthManager events
        HealthManager.Instance.OnHealthChanged.AddListener(SetHealth);
        SetMaxHealth(HealthManager.Instance.maxHealth);
        SetHealth(HealthManager.Instance.CurrentHealth);
    }

    void OnDestroy()
    {
        if (HealthManager.Instance != null)
            HealthManager.Instance.OnHealthChanged.RemoveListener(SetHealth);
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
