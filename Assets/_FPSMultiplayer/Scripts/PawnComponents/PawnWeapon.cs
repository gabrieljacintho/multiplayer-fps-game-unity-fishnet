using System.Collections;
using FishNet.Object;
using UnityEngine;

namespace FPSMultiplayer.PawnComponents
{
    public sealed class PawnWeapon : NetworkBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _damage = 5f;
        [SerializeField] private float _shotDelay = 0.2f;

        private Pawn _pawn;
        private PawnInput _input;

        private float _timeUntilNextShot;


        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            _pawn = GetComponent<Pawn>();
            _input = GetComponent<PawnInput>();
        }

        private void Update()
        {
            if (!IsOwner)
            {
                return;
            }

            if (_timeUntilNextShot <= 0f)
            {
                if (_input.Fire)
                {
                    ServerFire(_firePoint.position, _firePoint.forward);
                    _timeUntilNextShot = _shotDelay;
                }
            }
            else
            {
                _timeUntilNextShot -= Time.deltaTime;
            }

        }

        [ServerRpc]
        private void ServerFire(Vector2 origin, Vector2 direction)
        {
            if (Physics.Raycast(origin, direction, out RaycastHit hit) && hit.transform.TryGetComponent(out Pawn pawn))
            {
                pawn.ReceiveDamage(_damage);
            }
        }
    }
}