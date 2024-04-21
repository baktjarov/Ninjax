using UnityEngine;

namespace Gameplay
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _objectToFollow;
        [SerializeField] private Camera _cameraComponent;
        [SerializeField] private float _followSpeed = 10;
        [SerializeField] private Vector3 _followOffset;

        public void SetFollowObject(Transform objectToFollow)
        {
            _objectToFollow = objectToFollow;
        }

        private void Update()
        {
            if (_objectToFollow == null) { return; }

            Vector3 positionToFollow = _objectToFollow.position;
            Vector3 targetPosition = positionToFollow - _followOffset;

            _cameraComponent.transform.position = Vector3.Lerp(
                _cameraComponent.transform.position,
                targetPosition,
                _followSpeed * Time.deltaTime);
        }
    }
}