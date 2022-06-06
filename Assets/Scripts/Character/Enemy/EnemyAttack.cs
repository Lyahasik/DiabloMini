using UnityEngine;

using Weapon;
using Extension;

namespace Character.Enemy
{
    [RequireComponent(typeof(EnemyCharacteristics))]
    [RequireComponent(typeof(Animator))]
    public class EnemyAttack : MonoBehaviour
    {
        private const float DelayTimeReduction = 0.2f;
        
        private readonly int AnimationAttackId = Animator.StringToHash("Attack");
    
        private EnemyCharacteristics _enemyCharacteristics;
        private Animator _animator;
    
        [SerializeField] private GameObject _weapon;
        private WeaponAttack _weaponAttack;

        void Start()
        {
            _enemyCharacteristics = GetComponent<EnemyCharacteristics>();
            _animator = GetComponent<Animator>();
        
            _weaponAttack = _weapon.GetComponent<WeaponAttack>();
            _weaponAttack.Damage = _enemyCharacteristics.Damage;
            _weaponAttack.DelayAttack = _animator.FindAnimationClip("Attack").length - DelayTimeReduction;
        }

        public void StartAttack()
        {
            _weaponAttack.IsAttacking = true;
            _animator.SetBool(AnimationAttackId, true);
        }

        public void StopAttack()
        {
            _weaponAttack.IsAttacking = false;
            _animator.SetBool(AnimationAttackId, false);
        }
    }
}
