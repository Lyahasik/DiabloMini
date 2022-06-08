using UnityEngine;

using Characters.Player;
using Characters.Enemys;
using UI;

namespace Scene
{
    public class MouseClickSceneController : MonoBehaviour
    {
        private PlayerMoving _playerMoving;
        private PlayerPursuitTarget _pursuitTarget;
        private Camera _camera;

        private MainBar _mainBar;

        private void OnEnable()
        {
            EventManagerUIBasicCharacteristics.OnDiePlayer += DiePlayer;

            _mainBar = FindObjectOfType<MainBar>();
        }

        void Start()
        {
            _playerMoving = FindObjectOfType<PlayerMoving>();
            _pursuitTarget = FindObjectOfType<PlayerPursuitTarget>();
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
            if (_mainBar.HitMouse())
                return;
            
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

        private void DiePlayer()
        {
            GetComponent<MouseClickSceneController>().enabled = false;
        }
    }
}
