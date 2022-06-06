using UnityEngine;

namespace Scene
{
    public class CameraFixation : MonoBehaviour
    {
        private Transform _transformParent;
        
        private Vector3 _shiftPosition;
        private Quaternion _startTurn;

        private void Start()
        {
            _transformParent = transform.parent;
            
            _shiftPosition = _transformParent.position - transform.position;
            _startTurn = transform.rotation;
        }

        private void Update()
        {
            transform.position = _transformParent.position - _shiftPosition;
            transform.rotation = _startTurn;
        }
    }
}
