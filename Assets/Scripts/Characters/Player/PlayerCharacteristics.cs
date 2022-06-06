using System;
using System.Collections;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerCharacteristics : MonoBehaviour, IAttackingCharacteristics, ITakingDamage
    {
        private const float DelayLossRage = 1.0f;
        
        [SerializeField] private int maxHealth;
        [SerializeField] private int maxRage;
        [SerializeField] private int furyPerSecond;
        
        [SerializeField] private int damage;
        public int MaxHealth => maxHealth;
        public int MaxRage => maxRage;
        public int Damage => damage;

        private int _currentHealth;
        private int _currentRage;

        private void OnEnable()
        {
            EventManagerUIBasicCharacteristics.OnReduceHealth += ReduceHealth;
            EventManagerUIBasicCharacteristics.OnIncreaseRage += IncreaseRage;
            EventManagerUIBasicCharacteristics.OnReduceRage += ReduceRage;
        }

        private void OnDisable()
        {
            EventManagerUIBasicCharacteristics.OnReduceHealth -= ReduceHealth;
            EventManagerUIBasicCharacteristics.OnIncreaseRage -= IncreaseRage;
            EventManagerUIBasicCharacteristics.OnReduceRage -= ReduceRage;
        }

        private void Start()
        {
            _currentHealth = maxHealth;
            _currentRage = 0;

            StartCoroutine(PermanentLossRage());
        }

        private IEnumerator PermanentLossRage()
        {
            var condition = new WaitForSeconds(DelayLossRage);
            
            while (true)
            {
                yield return condition;
                
                EventManagerUIBasicCharacteristics.ReduceRage(furyPerSecond);
            }
        }

        public void ReduceHealth(int value)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - value, 0, maxHealth);
            
            if (_currentHealth <= 0)
                Die();
        }

        public void IncreaseRage(int value)
        {
            _currentRage = Mathf.Clamp(_currentRage + value, 0, maxRage);
        }

        public void ReduceRage(int value)
        {
            _currentRage = Mathf.Clamp(_currentRage - value, 0, maxRage);
        }

        public void TakeDamage(int value)
        {
            EventManagerUIBasicCharacteristics.ReduceHealth(value);
            EventManagerUIBasicCharacteristics.IncreaseRage(value);
        }

        private void Die()
        {
            EventManagerUIBasicCharacteristics.DiePlayer();
            
            Destroy(gameObject);
        }
    }
}
