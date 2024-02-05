using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters.MainPlayer
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _movementSpeed = 1;

        private InputHolder _inputHolder;
        private Vector2 _movementDirecrtion;

        private void Awake()
        {
            _inputHolder = new InputHolder();
            _inputHolder.Enable();
        }

        private void OnEnable()
        {
            _inputHolder.Player.Move.started += MoveStarted;
            _inputHolder.Player.Move.performed += MovePerformed;
            _inputHolder.Player.Move.canceled += MoveCancelled;
        }

        private void FixedUpdate()
        {
            Vector3 moveDirection = new Vector3(_movementDirecrtion.x, 0, _movementDirecrtion.y);
            _rigidbody.velocity = moveDirection * _movementSpeed;
        }

        private void MoveStarted(InputAction.CallbackContext context)
        {

        }

        private void MovePerformed(InputAction.CallbackContext context)
        {
            _movementDirecrtion = context.ReadValue<Vector2>();

            Vector3 movementVector = new Vector3(_movementDirecrtion.x, 0, _movementDirecrtion.y);
            transform.DOKill();
            transform.DORotateQuaternion(Quaternion.LookRotation(movementVector, Vector3.up), 0.5f);
        }

        private void MoveCancelled(InputAction.CallbackContext context)
        {
            _movementDirecrtion = Vector2.zero;
        }

        private void OnDisable()
        {

        }
    }
}