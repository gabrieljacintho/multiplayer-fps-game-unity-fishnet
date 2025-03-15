using FishNet.Object;
using UnityEngine;

namespace FPSMultiplayer
{
    public class PawnMovement : NetworkBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _jumpSeed = 1f;
        [SerializeField] private float _gravityScale = 1f;

        private CharacterController _characterController;
        private PawnInput _input;

        private Vector3 _velocity;


        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            _characterController = GetComponent<CharacterController>();
            _input = GetComponent<PawnInput>();
        }

        private void Update()
        {
            if (!IsOwner)
            {
                return;
            }

            Vector3 input = (transform.forward * _input.Vertical) + (transform.right * _input.Horizontal);
            Vector3 desiredVelocity = Vector3.ClampMagnitude(input * _speed, _speed);

            _velocity.x = desiredVelocity.x;
            _velocity.z = desiredVelocity.z;

            if (_characterController.isGrounded)
            {
                _velocity.y = 0f;

                if (_input.Jump)
                {
                    _velocity.y = _jumpSeed;
                }
            }
            else
            {
                _velocity.y += Physics.gravity.y * _gravityScale * Time.deltaTime;
            }

            _characterController.Move(_velocity * Time.deltaTime);
        }
    }
}
