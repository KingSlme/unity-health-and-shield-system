using UnityEngine;
using System;

public class HealthAndShieldSystem : MonoBehaviour
{   
    [Header("Health")]
    [SerializeField] private float _healthMax = 100.0f;
    [SerializeField] [Tooltip("Defaults to _healthMax")] private float _healthStarting;
    private float _healthCurrent;
    [Header("Shield")]
    [SerializeField] private float _shieldMax = 100.0f;
    [SerializeField] private float _shieldStarting;
    private float _shieldCurrent;

    public event EventHandler OnHealthMaxChanged;
    public event EventHandler OnHealthCurrentChanged;
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDead;

    public event EventHandler OnShieldMaxChanged;
    public event EventHandler OnShieldCurrentChanged;
    public event EventHandler OnShieldDamaged;
    public event EventHandler OnShieldRegenerated;
    public event EventHandler OnShieldBroken;

    private void Awake()
    {
        _shieldCurrent = _shieldStarting;
        if (_healthStarting > 0.0f)
            _healthCurrent = _healthStarting;
        else
            _healthCurrent = _healthMax;
    }

    public void Damage(float amount)
    {
        if (amount <= 0.0f)
        {
            Debug.LogError("Damage must be a positive float!");
            return;
        }
        if (amount < _shieldCurrent) 
        {
            _shieldCurrent -= amount;
            OnShieldCurrentChanged?.Invoke(this, EventArgs.Empty);
            OnShieldDamaged?.Invoke(this, EventArgs.Empty);
        }
        else 
        {   
            if (_shieldCurrent > 0.0f)
            {
                float leftOverDamage = amount - _shieldCurrent;
                _shieldCurrent = 0.0f;
                OnShieldCurrentChanged?.Invoke(this, EventArgs.Empty);
                OnShieldDamaged?.Invoke(this, EventArgs.Empty);
                BreakShield();
                _healthCurrent -= leftOverDamage;
                if (_healthCurrent < 0.0f)
                    _healthCurrent = 0.0f;
                OnHealthCurrentChanged?.Invoke(this, EventArgs.Empty);
                OnDamaged?.Invoke(this, EventArgs.Empty);
                if (_healthCurrent <= 0.0f)
                    Die();
            }
            else 
            {
                _healthCurrent -= amount;
                if (_healthCurrent < 0.0f)
                    _healthCurrent = 0.0f;
                OnHealthCurrentChanged?.Invoke(this, EventArgs.Empty);
                OnDamaged?.Invoke(this, EventArgs.Empty);
                if (_healthCurrent <= 0.0f)
                    Die();
            }
        }
    }

    /* Health Methods */
    public float GetHealthMax() => _healthMax;
    public float GetHealthCurrent() => _healthCurrent;
    public float GetHealthCurrentNormalized() => _healthCurrent / _healthMax;

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

    /* Shield Methods */
    public float GetShieldMax() => _shieldMax;
    public float GetShieldCurrent() => _shieldCurrent;
    public float GetShieldCurrentNormalized() => _shieldCurrent / _shieldMax;
    
    public void RegenerateShield(float amount)
    {
        _shieldCurrent += amount;
        if (_shieldCurrent > _shieldMax)
            _shieldCurrent = _shieldMax;
        OnShieldCurrentChanged?.Invoke(this, EventArgs.Empty);
        OnShieldRegenerated?.Invoke(this, EventArgs.Empty);
    }

    public void RegenerateShieldFully()
    {
        _shieldCurrent = _shieldMax;
        OnShieldCurrentChanged?.Invoke(this, EventArgs.Empty);
        OnShieldRegenerated?.Invoke(this, EventArgs.Empty);
    }

    public void SetShieldMax(float newShieldMax, bool regenerateToShieldMax)
    {
        _shieldMax = newShieldMax;
        if (regenerateToShieldMax)
            _shieldCurrent = newShieldMax;
        OnShieldMaxChanged?.Invoke(this, EventArgs.Empty);
        OnShieldCurrentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetShieldCurrent(float newShield) 
    {
        if (newShield > _shieldMax)
            _shieldCurrent = _shieldMax;
        else if (newShield < 0.0f)
            _shieldCurrent = 0.0f;
        else
            _shieldCurrent = newShield;
        OnShieldCurrentChanged?.Invoke(this, EventArgs.Empty);
        if (_shieldCurrent <= 0.0f)
            BreakShield();
    }

    public bool IsShieldBroken() => _shieldCurrent <= 0.0f;

    private void BreakShield() => OnShieldBroken?.Invoke(this, EventArgs.Empty);
}
