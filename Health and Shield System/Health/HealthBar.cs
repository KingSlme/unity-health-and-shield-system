using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private Image _image;

    void Start()
    {
        UpdateHealthBar();
        _healthSystem.OnHealthCurrentChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) => UpdateHealthBar();
    private void UpdateHealthBar() => _image.fillAmount = _healthSystem.GetHealthCurrentNormalized();
    private void OnDestroy() => _healthSystem.OnHealthCurrentChanged -= HealthSystem_OnHealthChanged;
}