using UnityEngine;
using UnityEngine.AI;

using Characters.Player;

namespace Characters.Enemys
{
    public class EnemyPursuitTarget : PursuitTarget
    {
        private bool _isTargetAchieved;
        private bool _isLockedTarget;

        [SerializeField] private float _visibilityDistance;
        [SerializeField] private float _viewingAngle;

        private void Start()
        {
            _attack = GetComponent<Attack>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();

            _target = FindObjectOfType<PlayerCharacteristics>().gameObject;
        }

        private void Update()
        {
            if (_isLockedTarget)
            {
                CheckTargetBeenReached();
                Pursue();
            }
            TrySeePlayer();
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
