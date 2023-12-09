using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{   
    [SerializeField] private float _healthMax = 100.0f;
    [SerializeField] private float _healthStarting;
    private float _healthCurrent;

    public event EventHandler OnHealthMaxChanged;
    public event EventHandler OnHealthCurrentChanged;
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDead;

    private void Awake()
    {
        if (_healthStarting != 0.0f)
            _healthCurrent = _healthMax;
    }

    public float GetHealthMax() => _healthMax;
    public float GetHealthCurrent() => _healthCurrent;
    public float GetHealthCurrentNormalized() => _healthCurrent / _healthMax;

    public void Damage(float amount)
    {
        _healthCurrent -= amount;
        if (_healthCurrent < 0.0f)
            _healthCurrent = 0.0f;
        OnHealthCurrentChanged?.Invoke(this, EventArgs.Empty);
        OnDamaged?.Invoke(this, EventArgs.Empty);
        if (_healthCurrent <= 0.0f)
            Die();
    }

    public void Heal(float amount)
    {
        _healthCurrent += amount;
        if (_healthCurrent > _healthMax)
            _healthCurrent = _healthMax;
        OnHealthCurrentChanged?.Invoke(this, EventArgs.Empty);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealFully()
    {
        _healthCurrent = _healthMax;
        OnHealthCurrentChanged?.Invoke(this, EventArgs.Empty);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void SetHealthMax(float newHealthMax, bool healToHealthMax)
    {
        _healthMax = newHealthMax;
        if (healToHealthMax)
            _healthCurrent = newHealthMax;
        OnHealthMaxChanged?.Invoke(this, EventArgs.Empty);
        OnHealthCurrentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetHealthCurrent(float newHealth) 
    {
        if (newHealth > _healthMax)
            _healthCurrent = _healthMax;
        else if (newHealth < 0.0f)
            _healthCurrent = 0.0f;
        else
            _healthCurrent = newHealth;
        OnHealthCurrentChanged?.Invoke(this, EventArgs.Empty);
        if (_healthCurrent <= 0.0f)
            Die();
    }

    public bool IsDead() => _healthCurrent <= 0.0f;
    
    private void Die() => OnDead?.Invoke(this, EventArgs.Empty);
}