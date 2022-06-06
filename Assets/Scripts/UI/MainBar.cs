using Characters.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainBar : MonoBehaviour
    {
        [SerializeField] private Image valueImageHealthBar;
        [SerializeField] private Image valueImageRageBar;
        
        private int _maxHealth;
        private int _currentHealth;
        
        private int _maxRage;
        private int _currentRage;

        private void OnEnable()
        {
            EventManagerUIBasicCharacteristics.OnReduceHealth += ReduceHealth;
            EventManagerUIBasicCharacteristics.OnIncreaseRage += IncreaseRage;
            EventManagerUIBasicCharacteristics.OnReduceRage += ReduceRage;
        }

        private void Start()
        {
            PlayerCharacteristics playerCharacteristics = FindObjectOfType<PlayerCharacteristics>();
            
            _maxHealth = playerCharacteristics.MaxHealth;
            _currentHealth = _maxHealth;
            
            _maxRage = playerCharacteristics.MaxRage;
            _currentRage = 0;
        }

        private void ReduceHealth(int value)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - value, 0, _maxHealth);
            
            valueImageHealthBar.fillAmount = (float) _currentHealth / _maxHealth;
        }

        private void IncreaseRage(int value)
        {
            _currentRage = Mathf.Clamp(_currentRage + value, 0, _maxRage);
            
            valueImageRageBar.fillAmount = (float) _currentRage / _maxRage;
        }

        private void ReduceRage(int value)
        {
            _currentRage = Mathf.Clamp(_currentRage - value, 0, _maxRage);
            
            valueImageRageBar.fillAmount = (float) _currentRage / _maxRage;
        }
    }
}
