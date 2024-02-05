using UnityEngine;

namespace Gameplay
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _objectToFollow;
        [SerializeField] private Camera _cameraComponent;
        [SerializeField] private float _followSpeed = 1;
        [SerializeField] private Vector3 _followOffset;

        private void Update()
        {
            Vector3 positionToFollow = _objectToFollow.position;
            Vector3 targetPosition = positionToFollow - _followOffset;

            _cameraComponent.transform.position = Vector3.Lerp(_cameraComponent.transform.position, targetPosition,
            _followSpeed * Time.deltaTime);
        }
    }
}