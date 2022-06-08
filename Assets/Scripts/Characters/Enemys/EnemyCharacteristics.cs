using System;
using Characters.Player;
using UnityEngine;

namespace Characters.Enemys
{
    [RequireComponent(typeof(RagdollActivation))]
    [RequireComponent(typeof(PursuitTarget))]
    public class EnemyCharacteristics : MonoBehaviour, IAttackingCharacteristics, ITakingDamage
    {
        private RagdollActivation _ragdollActivation;
        private PursuitTarget _pursuitTarget;
        
        [SerializeField] private int health;
        
        [SerializeField] private int damage;
        public int Damage => damage;
        
        [SerializeField] private float delayDie;

        private void Start()
        {
            _ragdollActivation = GetComponent<RagdollActivation>();
            _pursuitTarget = GetComponent<PursuitTarget>();
        }

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
            FindObjectOfType<PlayerPursuitTarget>().ResetTarget();
            _ragdollActivation.Activate();
            _pursuitTarget.IsFreeze = true;
            
            Destroy(gameObject, delayDie);
        }
    }
}