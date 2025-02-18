using UnityEngine;

namespace Platformer2D
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _player;

        private void OnEnable()
        {
            if (_player != null)
            {
                Health playerHealth = _player.GetComponent<Health>();
                playerHealth.Died += OnPlayerDied;
            }
        }

        private void LateUpdate()
        {
            if (_player != null)
            {
                Follow(_player);
            }
        }

        private void OnDisable()
        {
            if (_player != null)
            {
                Health playerHealth = _player.GetComponent<Health>();
                playerHealth.Died += OnPlayerDied;
            }
        }

        private void Follow(Transform target)
        {
            Vector3 desiredPosition = target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }

        private void OnPlayerDied()
        {
            _player = null;
        }
    }
}
