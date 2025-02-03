using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour, IService
{
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private TextMeshProUGUI _healthAmountText;

    private int _maxHealth;

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        _maxHealth = Mathf.Clamp(_maxHealth, IAlive.MinHealthPoint, IAlive.MaxHealthPoint);  
    }

    public void UpdateUI(int healthAmount)
    {
        float fillAmount = Mathf.Clamp01((float)healthAmount / _maxHealth);
        _healthBarFill.fillAmount = fillAmount;
        _healthAmountText.text = $"{healthAmount}/{_maxHealth}";
    }
}
