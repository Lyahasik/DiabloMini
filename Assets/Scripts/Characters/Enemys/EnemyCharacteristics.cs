using UnityEngine;

namespace Characters.Enemys
{
    public class EnemyCharacteristics : MonoBehaviour, IAttackingCharacteristics, ITakingDamage
    {
        [SerializeField] private int health;
        
        [SerializeField] private int damage;
        public int Damage => damage;

        public void TakeDamage(int value)
        {
            health -= value;
            
            EventManagerUIBasicCharacteristics.IncreaseRage(value);
            Debug.Log("Enemy: " + health);
            
            if (health <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}