using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMovingController : MonoBehaviour
    {
        private readonly int AnimationMoveId  = Animator.StringToHash("Move");
        
        private Camera _camera;
        
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private bool _isMoving;

        private void Start()
        {
            _camera = Camera.main;
            
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            DestinationBeenReached();
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SetDestination();
            }
        }

        private void SetDestination()
        {
            Ray rayMouseClick = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(rayMouseClick, out hit))
            {
                _navMeshAgent.SetDestination(hit.point);
                
                _animator.SetBool(AnimationMoveId, true);
                _isMoving = true;
            }
        }

        private void DestinationBeenReached()
        {
            if (!_isMoving)
                return;

            if (!Single.IsInfinity(_navMeshAgent.remainingDistance)
                && _navMeshAgent.remainingDistance <= 0.5f)
            {
                _animator.SetBool(AnimationMoveId, false);
                _isMoving = false;
            }
        }
    }
}
