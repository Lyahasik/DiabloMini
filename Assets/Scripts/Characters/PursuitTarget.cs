using UnityEngine;
using UnityEngine.AI;

namespace Characters
{
    [RequireComponent(typeof(Attack))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class PursuitTarget : MonoBehaviour
    {
        private readonly int AnimationMoveId = Animator.StringToHash("Move");

        protected NavMeshAgent _navMeshAgent;
        protected Animator _animator;
        protected Attack _attack;
        
        protected GameObject _target;
        protected bool _isTargetAchieved;
        
        protected bool _isFreeze;
        public bool IsFreeze
        {
            set
            {
                _isFreeze = value;
                
                _navMeshAgent.isStopped = _isTargetAchieved ? true : _isFreeze;
            }
        }
        
        [SerializeField] private float stoppingDistance;

        protected void Pursue()
        {
            if (_isFreeze
                || _isTargetAchieved)
                return;

            _navMeshAgent.SetDestination(_target.transform.position);
        }

        protected void CheckTargetBeenReached()
        {
            if (_isFreeze)
                return;
            
            if (_isTargetAchieved)
            {
                float distantion = Vector3.Magnitude(_target.transform.position - transform.position);
            
                if (distantion > stoppingDistance)
                {
                    StartMoving();
                }
            }
            else if (!_navMeshAgent.pathPending
                     && _navMeshAgent.remainingDistance <= stoppingDistance)
            {
                StopMoving();
            }
        }

        protected void StartMoving()
        {
            _navMeshAgent.isStopped = false;
            _animator.SetBool(AnimationMoveId, true);
            _isTargetAchieved = false;

            _attack.ResetTarget();
        }

        protected void StopMoving()
        {
            _navMeshAgent.isStopped = true;
            _animator.SetBool(AnimationMoveId, false);
            _isTargetAchieved = true;
            
            transform.LookAt(_target.transform);
            _attack.SetTarget(_target);
        }
    }
}
