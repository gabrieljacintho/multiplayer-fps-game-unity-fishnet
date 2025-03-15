using FishNet.Object;
using UnityEngine;

namespace FPSMultiplayer
{
    public sealed class PawnInput : NetworkBehaviour
    {
        public float Horizontal;
        public float Vertical;

        public float MouseX;
        public float MouseY;

        public float Sensitivity = 1f;

        public bool Jump;
        public bool Fire;


        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
        }

        private void Update()
        {
            if (!IsOwner)
            {
                return;
            }

            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");

            MouseX = Input.GetAxis("Mouse X") * Sensitivity;
            MouseY = Input.GetAxis("Mouse Y") * Sensitivity;

            Jump = Input.GetButton("Jump");

            Fire = Input.GetButton("Fire1");
        }
    }
}