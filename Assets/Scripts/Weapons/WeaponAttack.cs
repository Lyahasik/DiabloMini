using UnityEngine;

using Characters;
using Characters.Enemys;

namespace Weapons
{
    public class WeaponAttack : MonoBehaviour
    {
        private int _damage;
        public int Damage
        {
            set => _damage = value;
        }

        private float _delayAttack;
        public float DelayAttack
        {
            set => _delayAttack = value;
        }
        
        private bool _isAttacking;
        public bool IsAttacking
        {
            set => _isAttacking = value;
        }
        
        private bool _isDealsDamage;

        private void Awake()
        {
            _isDealsDamage = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isAttacking && other.GetComponent<EnemyCharacteristics>()
                || !_isDealsDamage)
                return;
            
            ITakingDamage takingDamage = other.GetComponent<ITakingDamage>();
            if (takingDamage != null)
            {
                takingDamage.TakeDamage(_damage);
                
                _isDealsDamage = false;
                Invoke(nameof(Recharge), _delayAttack);
            }
        }

        private void Recharge()
        {
            _isDealsDamage = true;
        }
    }
}
