using UnityEngine;

using Weapons;
using Extensions;

namespace Characters
{
    [RequireComponent(typeof(IAttackingCharacteristics))]
    [RequireComponent(typeof(Animator))]
    public class Attack : MonoBehaviour
    {
        private const float DelayTimeReduction = 0.2f;
        
        private readonly int AnimationAttackId = Animator.StringToHash("Attack");
    
        private IAttackingCharacteristics _attackingCharacteristics;
        private Animator _animator;
    
        [SerializeField] private GameObject weapon;
        private WeaponAttack _weaponAttack;

        private GameObject _target;

        void Start()
        {
            _attackingCharacteristics = GetComponent<IAttackingCharacteristics>();
            _animator = GetComponent<Animator>();
        
            _weaponAttack = weapon.GetComponent<WeaponAttack>();
            _weaponAttack.Damage = _attackingCharacteristics.Damage;
            _weaponAttack.DelayAttack = _animator.FindAnimationClip("Attack").length - DelayTimeReduction;
        }

        private void Update()
        {
            CheckTarget();
        }

        private void CheckTarget()
        {
            if (!_target)
                StopAttack();
        }

        public void SetTarget(GameObject gameObject)
        {
            _target = gameObject;
            
            StartAttack();
        }

        public void ResetTarget()
        {
            _target = null;
        }

        private void StartAttack()
        {
            if (!_target)
                return;
            
            _weaponAttack.IsAttacking = true;
            _animator.SetBool(AnimationAttackId, true);
        }

        private void StopAttack()
        {
            _weaponAttack.IsAttacking = false;
            _animator.SetBool(AnimationAttackId, false);
        }
    }
}
