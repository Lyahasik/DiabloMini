using UnityEngine;

namespace Character.Player
{
    public class PlayerCharacteristics : MonoBehaviour, ITakingDamage
    {
        [SerializeField] private int _maxHealth;
        
        [SerializeField] private int _damage;
        public int Damage => _damage;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int value)
        {
            _currentHealth -= value;
            Debug.Log("Player: " + _currentHealth);
            if (_currentHealth <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
