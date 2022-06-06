using UnityEngine;
using UnityEngine.AI;

namespace Characters.Player
{
    public class PlayerPursuitTarget : PursuitTarget
    {
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _attack = GetComponent<Attack>();
        }

        private void Update()
        {
            if (!_target)
                return;
            
            CheckTargetBeenReached();
            Pursue();
        }

        public void SetTarget(GameObject gameObject)
        {
            _target = gameObject;
            
            StartMoving();
        }

        public void ResetTarget()
        {
            _target = null;
            _attack.ResetTarget();
            _isTargetAchieved = false;
        }
    }
}
