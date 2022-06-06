using UnityEngine;

namespace Characters.Enemys
{
    public class EnemyCharacteristics : MonoBehaviour, IAttackingCharacteristics, ITakingDamage
    {
        [SerializeField] private int _health;
        
        [SerializeField] private int _damage;
        public int Damage => _damage;

        public void TakeDamage(int value)
        {
            _health -= value;
            
            Debug.Log("Enemy: " + _health);
            
            if (_health <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}