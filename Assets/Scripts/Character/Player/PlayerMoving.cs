using System;
using UnityEngine;
using UnityEngine.AI;

using Character.Enemy;

namespace Character.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMoving : MonoBehaviour
    {
        private readonly int AnimationMoveId = Animator.StringToHash("Move");
        private const float StoppingDistance = 1.0f;

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private bool _isMoving;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            DestinationBeenReached();
        }

        private void DestinationBeenReached()
        {
            if (!_isMoving)
                return;
            
            if (!_navMeshAgent.pathPending
                && !Single.IsInfinity(_navMeshAgent.remainingDistance)
                && _navMeshAgent.remainingDistance <= StoppingDistance)
            {
                StopMoving();
            }
        }

        public void SetDestination(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
            
            StartMoving();
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
    }
}
