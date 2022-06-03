using Player;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class EnemyMoving : MonoBehaviour
    {
        private readonly int AnimationMoveId  = Animator.StringToHash("Move");

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private GameObject _player;

        private bool _isMoving;
        private bool _isLockedTarget;

        [SerializeField] private float _visibilityDistance;
        [SerializeField] private float _viewingAngle;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();

            _player = FindObjectOfType<PlayerMovingController>().gameObject;

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
            if (_isMoving)
            {
                if (_navMeshAgent.remainingDistance > 3.0f)
                    return;

                StopMoving();
            }
            else if (_isLockedTarget)
            {
                float distantion = Vector3.Magnitude(_player.transform.position - transform.position);
            
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
            
            Vector3 directionPlayer = _player.transform.position - transform.position;
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
            if (!_isMoving)
                return;
            
            _navMeshAgent.SetDestination(_player.transform.position);
        }

        private void StartMoving()
        {
            _navMeshAgent.isStopped = false;
            
            _animator.SetBool(AnimationMoveId, true);
            
            _isMoving = true;
        }

        private void StopMoving()
        {
            _navMeshAgent.isStopped = true;
            
            _animator.SetBool(AnimationMoveId, false);
            
            _isMoving = false;
        }

        private bool OverlappingView()
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, _player.transform.position, out hit)
                && hit.transform.name == _player.transform.name)
            {
                return true;
            }

            return false;
        }
    }
}
