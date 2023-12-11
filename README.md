# unity-health-and-shield-system
A modular health and shield system for Unity.

## Key Features
- Use of event callbacks for loose coupling 
- Health bar prefabs already set up for both UI and World Space
- Option to hide health/shield bars until damaged

## Setup
1. Add the HealthSystem or HealthAndShieldSystem script to a GameObject
2. Add the desired UI prefab to a Canvas or World prefab to a GameObject and set its system reference in the inspector
3. Subscribe methods to the desired events

## Health Events
- OnHealthMaxChanged
- OnHealthCurrentChanged
- OnDamaged
- OnHealed
- OnDead

## Shield Events
- OnShieldMaxChanged
- OnShieldCurrentChanged
- OnShieldDamaged
- OnShieldRegenerated
- OnShieldBroken

## General Methods

### Damage
*Damages the system.*
```cs
public void Damage(float amount)
```

## Health Methods

### GetHealthMax
*Returns the max health.*
```cs
public float GetHealthMax()
```

### GetHealthCurrent
*Returns the current health.*
```cs
public float GetHealthCurrent()
```

### GetHealthCurrentNormalized
*Returns the current health normalized.*
```cs
public float GetHealthCurrentNormalized()
```

### Heal
*Heals the system's health.*
```cs
public void Heal(float amount)
```

### HealFully
*Fully heals the system's health.*
```cs
public void HealFully()
```

### SetHealthMax
*Sets the system's max health.*
```cs
public void SetHealthMax(float newHealthMax, bool healToHealthMax)
```

### SetHealthCurrent
*Sets the system's current health.*
```cs
public void SetHealthCurrent(float newHealth)
```

### IsDead
*Returns whether the system is dead.*
```cs
public bool IsDead()
```

## Shield Methods

### GetShieldMax
*Returns the max shield.*
```cs
public float GetShieldMax()
```

### GetShieldCurrent
*Returns the current shield.*
```cs
public float GetShieldCurrent()
```

### GetShieldCurrentNormalized
*Returns the current shield normalized.*
```cs
public float GetShieldCurrentNormalized()
```

### RegenerateShield
*Regenerates the system's shield.*
```cs
public void RegenerateShield(float amount)
```

### RegenerateShieldFully
*Fully regenerates the system's shield.*
```cs
public void RegenerateShieldFully()
```

### SetShieldMax
*Sets the system's max shield.*
```cs
public void SetShieldMax(float newShieldMax, bool regenerateToShieldMax)
```

### SetShieldCurrent
*Sets the system's current shield.*
```cs
public void SetShieldCurrent(float newShield)
```

### IsShieldBroken
*Returns whether the system's shield is broken.*
```cs
public bool IsShieldBroken()
```

## Dependencies
- None
