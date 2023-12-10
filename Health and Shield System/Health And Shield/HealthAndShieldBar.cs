using UnityEngine;
using UnityEngine.UI;

public class HealthAndShieldBar : MonoBehaviour
{   
    [SerializeField] private HealthAndShieldSystem _healthAndShieldSystem;
    [SerializeField] private Image _healthBackground;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _shieldBackground;
    [SerializeField] private Image _shieldBar;
    [SerializeField] private bool _hideUntilDamaged = false;

    void Start()
    {
        UpdateHealthBar();
        UpdateShieldBar();
        _healthAndShieldSystem.OnHealthCurrentChanged += HealthAndShieldSystem_OnHealthChanged;
        _healthAndShieldSystem.OnShieldCurrentChanged += HealthAndShieldSystem_OnShieldChanged;
        if (_hideUntilDamaged)
        {
            ToggleHealthBar(false);
            ToggleShieldBar(false);
        }
    }

    private void HealthAndShieldSystem_OnHealthChanged(object sender, System.EventArgs e) => UpdateHealthBar();
    private void HealthAndShieldSystem_OnShieldChanged(object sender, System.EventArgs e) => UpdateShieldBar();
    
    private void UpdateHealthBar()
    {
        _healthBar.fillAmount = _healthAndShieldSystem.GetHealthCurrentNormalized();
        if (_healthAndShieldSystem.GetHealthCurrent() > 0.0f)
            ToggleHealthBar(true);
        else
            ToggleHealthBar(false);
    }
    private void UpdateShieldBar()
    {
        _shieldBar.fillAmount = _healthAndShieldSystem.GetShieldCurrentNormalized();
        if (_healthAndShieldSystem.GetShieldCurrent() > 0.0f)
            ToggleShieldBar(true);
        else
            ToggleShieldBar(false);
    }

    private void ToggleHealthBar(bool visible)
    {
        _healthBackground.gameObject.SetActive(visible);
        _healthBar.gameObject.SetActive(visible);
    }
    private void ToggleShieldBar(bool visible)
    {
        _shieldBackground.gameObject.SetActive(visible);
        _shieldBar.gameObject.SetActive(visible);

    }

    private void OnDestroy()
    {
        _healthAndShieldSystem.OnHealthCurrentChanged -= HealthAndShieldSystem_OnHealthChanged;
        _healthAndShieldSystem.OnShieldCurrentChanged -= HealthAndShieldSystem_OnShieldChanged;
    }
}
