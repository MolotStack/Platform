using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public UnityAction OnDied;
    public UnityAction<float> OnChangedHealth;
    public UnityAction<float> OnChangedMaxHealth;

    public float CurrentHealth => _currentHealth;
    public float CurrentMaxHealth => _currentMaxHealth;

    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private float _currentMaxHealth;

    public void ChangeHealth(float deltaHealth)
    {
        _currentHealth += deltaHealth;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDied?.Invoke();
        }
        else if (_currentHealth >= _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }

        OnChangedHealth?.Invoke(_currentHealth);
    }

    public void ChangeMaxHealth(float deltaHealth)
    {
        _currentMaxHealth += deltaHealth;

        if ( _currentMaxHealth <= 10)
        {
            _currentMaxHealth = 10;
        }

        OnChangedMaxHealth?.Invoke(_currentMaxHealth);
    }
}
