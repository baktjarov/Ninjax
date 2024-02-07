using DataClasses;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters.MainPlayer
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _movementSpeed = 1;
        [SerializeField] private Animator _animator;

        [Header("Debug")]
        [SerializeField] public ValueSignal<bool> isMoving;

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

        private void OnDisable()
        {

        }

        private void FixedUpdate()
        {
            Vector3 moveDirection = new Vector3(_movementDirecrtion.x, 0, _movementDirecrtion.y);
            _rigidbody.velocity = moveDirection * _movementSpeed;
        }

        private void MoveStarted(InputAction.CallbackContext context)
        {
            isMoving.ChangeValue(true);
        }

        private void MovePerformed(InputAction.CallbackContext context)
        {
            _movementDirecrtion = context.ReadValue<Vector2>();

            Vector3 movementVector = new Vector3(_movementDirecrtion.x, 0, _movementDirecrtion.y);
            _animator.transform.DORotateQuaternion(Quaternion.LookRotation(movementVector, Vector3.up), 0.5f);

            _animator.SetFloat("Forward", _movementDirecrtion.magnitude);
        }

        private void MoveCancelled(InputAction.CallbackContext context)
        {
            _movementDirecrtion = Vector2.zero;
            _animator.SetFloat("Forward", 0);

            isMoving.ChangeValue(false);
        }
    }
}