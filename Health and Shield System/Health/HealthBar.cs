using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private Image _healthBackground;
    [SerializeField] private Image _healthBar;
    [SerializeField] private bool _hideUntilDamaged = false;

    void Start()
    {
        UpdateHealthBar();
        _healthSystem.OnHealthCurrentChanged += HealthSystem_OnHealthChanged;
        if (_hideUntilDamaged)
            ToggleHealthBar(false);
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) => UpdateHealthBar();

    private void UpdateHealthBar()
    {
        _healthBar.fillAmount = _healthSystem.GetHealthCurrentNormalized();
        if (_healthSystem.GetHealthCurrent() > 0.0f)
            ToggleHealthBar(true);
        else
            ToggleHealthBar(false);
    }

    private void ToggleHealthBar(bool visible)
    {
        _healthBackground.gameObject.SetActive(visible);
        _healthBar.gameObject.SetActive(visible);
    }

    private void OnDestroy() => _healthSystem.OnHealthCurrentChanged -= HealthSystem_OnHealthChanged;
}