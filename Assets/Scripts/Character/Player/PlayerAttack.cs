using UnityEngine;

using Weapon;
using Extension;

namespace Character.Player
{
    [RequireComponent(typeof(PlayerCharacteristics))]
    [RequireComponent(typeof(Animator))]
    public class PlayerAttack : MonoBehaviour
    {
        private const float DelayTimeReduction = 0.2f;
        
        private readonly int AnimationAttackId = Animator.StringToHash("Attack");
        
        private PlayerCharacteristics _playerCharacteristics;
        private Animator _animator;
        
        [SerializeField] private GameObject _weapon;
        private WeaponAttack _weaponAttack;

        private GameObject _target;

        void Start()
        {
            _playerCharacteristics = GetComponent<PlayerCharacteristics>();
            _animator = GetComponent<Animator>();
            
            _weaponAttack = _weapon.GetComponent<WeaponAttack>();
            _weaponAttack.Damage = _playerCharacteristics.Damage;
            _weaponAttack.DelayAttack = _animator.FindAnimationClip("Attack").length - DelayTimeReduction;
        }

        private void Update()
        {
            CheckTarget();
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

        private void CheckTarget()
        {
            if (!_target)
                StopAttack();
        }

        public void StartAttack()
        {
            if (!_target)
                return;
            
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
