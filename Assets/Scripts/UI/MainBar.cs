using Characters.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainBar : MonoBehaviour
    {
        [SerializeField] private Image valueImageHealthBar;
        [SerializeField] private Image valueImageRageBar;

        private RectTransform _rectTransform;
        private float _leftBorder;
        private float _rightBorder;
        
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
            _rectTransform = GetComponent<RectTransform>();
            _leftBorder = Screen.width * 0.5f - _rectTransform.rect.width * 0.5f;
            _rightBorder = Screen.width * 0.5f + _rectTransform.rect.width * 0.5f;
            
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

        public bool HitMouse()
        {
            Vector2 mousePosition = Input.mousePosition;

            if (_rectTransform.rect.height > mousePosition.y
                && mousePosition.x > _leftBorder
                && mousePosition.x < _rightBorder)
                return true;
            
            return false;
        }
    }
}
