using UnityEngine;
using UnityEngine.AI;

using Character.Player;

namespace Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerAttack))]
    public class PursuitTarget : MonoBehaviour
    {
        private readonly int AnimationMoveId = Animator.StringToHash("Move");
        private const float StoppingDistance = 3.3f;

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private PlayerAttack _playerAttack;
        
        private GameObject _target;
        private bool _isTargetAchieved;
        
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _playerAttack = GetComponent<PlayerAttack>();
        }

        private void Update()
        {
            if (!_target)
                return;
            
            Pursue();
            CheckTargetBeenReached();
        }

        private void Pursue()
        {
            if (_isTargetAchieved)
                return;

            StartMoving();
        }

        private void CheckTargetBeenReached()
        {
            if (_isTargetAchieved)
                return;

            if (!_navMeshAgent.pathPending
                && _navMeshAgent.remainingDistance <= StoppingDistance)
            {
                StopMoving();
            }
        }

        public void SetTarget(GameObject gameObject)
        {
            _target = gameObject;
        }

        public void ResetTarget()
        {
            _target = null;
            _playerAttack.ResetTarget();
            _isTargetAchieved = false;
        }

        private void StartMoving()
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_target.transform.position);
            _animator.SetBool(AnimationMoveId, true);
            _isTargetAchieved = false;

            _playerAttack.ResetTarget();
        }

        private void StopMoving()
        {
            _navMeshAgent.isStopped = true;
            _animator.SetBool(AnimationMoveId, false);
            _isTargetAchieved = true;
            
            transform.LookAt(_target.transform);
            _playerAttack.SetTarget(_target);
        }
    }
}
