using UnityEngine;
using UnityEngine.AI;

using Character.Player;

namespace Character.Enemy
{
    [RequireComponent(typeof(EnemyAttack))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class EnemyMoving : MonoBehaviour
    {
        private readonly int AnimationMoveId  = Animator.StringToHash("Move");

        private EnemyAttack _enemyAttack;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private GameObject _target;

        private bool _isTargetAchieved;
        private bool _isLockedTarget;

        [SerializeField] private float _visibilityDistance;
        [SerializeField] private float _viewingAngle;

        private void Start()
        {
            _enemyAttack = GetComponent<EnemyAttack>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();

            _target = FindObjectOfType<PlayerCharacteristics>().gameObject;

            _navMeshAgent.stoppingDistance = 1.0f;
        }
        
        private void Update()
        {
            DestinationBeenReached();
            TrySeePlayer();
            GoToPlayer();
        }

        private void DestinationBeenReached()
        {
            if (!_isTargetAchieved)
            {
                if (_navMeshAgent.remainingDistance > 3.0f)
                    return;

                StopMoving();
            }
            else if (_isLockedTarget)
            {
                float distantion = Vector3.Magnitude(_target.transform.position - transform.position);
            
                if (distantion > 3.0f)
                {
                    StartMoving();
                }
            }
        }

        private void TrySeePlayer()
        {
            if (_isLockedTarget)
                return;
            
            Vector3 directionPlayer = _target.transform.position - transform.position;
            directionPlayer.y = 0;
            
            float angle = Vector3.Angle(directionPlayer, transform.forward);

            if (Vector3.Magnitude(directionPlayer) < _visibilityDistance
                && angle < _viewingAngle
                && OverlappingView())
            {
                StartMoving();
                _isLockedTarget = true;
            }
        }

        private void GoToPlayer()
        {
            if (_isTargetAchieved)
                return;
            
            _navMeshAgent.SetDestination(_target.transform.position);
        }

        private void StartMoving()
        {
            _navMeshAgent.isStopped = false;
            _animator.SetBool(AnimationMoveId, true);
            _isTargetAchieved = false;
            
            _enemyAttack.StopAttack();
        }

        private void StopMoving()
        {
            transform.LookAt(_target.transform);
            _enemyAttack.StartAttack();
            
            _navMeshAgent.isStopped = true;
            _animator.SetBool(AnimationMoveId, false);
            _isTargetAchieved = true;
        }

        private bool OverlappingView()
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, _target.transform.position, out hit)
                && hit.transform.name == _target.transform.name)
            {
                return true;
            }

            return false;
        }
    }
}
