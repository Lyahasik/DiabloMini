using UnityEngine;

using Character;
using Character.Player;
using Character.Enemy;

namespace Scene
{
    public class MouseClickController : MonoBehaviour
    {
        private PlayerMoving _playerMoving;
        private PursuitTarget _pursuitTarget;
        private Camera _camera;
        
        void Start()
        {
            _playerMoving = FindObjectOfType<PlayerMoving>();
            _pursuitTarget = FindObjectOfType<PursuitTarget>();
            _camera = Camera.main;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CheckObjectHitPoint();
            }
        }

        private void CheckObjectHitPoint()
        {
            Ray rayMouseClick = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(rayMouseClick, out hit))
            {
                if (hit.collider.GetComponent<EnemyCharacteristics>())
                {
                    _pursuitTarget.SetTarget(hit.collider.gameObject);
                }
                else
                {
                    _pursuitTarget.ResetTarget();
                    _playerMoving.SetDestination(hit.point);
                }
            }
        }
    }
}
