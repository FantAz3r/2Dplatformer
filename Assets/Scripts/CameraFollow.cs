using UnityEngine;

namespace Platformer2D
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _player;

        private void LateUpdate()
        {
            Follow(_player);
        }

        private void Follow(Transform target)
        {
            Vector3 desiredPosition = target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
