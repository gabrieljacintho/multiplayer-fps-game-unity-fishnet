using System;
using FishNet.CodeGenerating;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FPSMultiplayer
{
    public sealed class Player : NetworkBehaviour
    {
        public static Player Instance { get; private set; }

        [AllowMutableSyncType, SerializeField] private SyncVar<string> _userName = new SyncVar<string>();
        [AllowMutableSyncType, SerializeField] private SyncVar<bool> _isReady = new SyncVar<bool>();
        [AllowMutableSyncType, SerializeField] private SyncVar<Pawn> _pawn = new SyncVar<Pawn>();

        public string UserName
        {
            get => _userName.Value;
            set => _userName.Value = value;
        }
        public bool IsReady
        {
            get => _isReady.Value;
            set => _isReady.Value = value;
        }
        public Pawn Pawn => _pawn.Value;


        public override void OnStartServer()
        {
            base.OnStartServer();
            GameManager.Instance.Players.Add(this);
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            GameManager.Instance.Players.Remove(this);
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            if (!IsOwner)
            {
                return;
            }

            Instance = this;

            ViewManager.Instance.Initialize();
            ViewManager.Instance.Show<LobbyView>();
        }

        private void Update()
        {
            if (!IsOwner)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ServerSetIsReady(!IsReady);
            }
        }

        public void StartGame()
        {
            GameObject pawnPrefab = Addressables.LoadAssetAsync<GameObject>("Pawn").WaitForCompletion(); // Not async

            GameObject pawnInstance = Instantiate(pawnPrefab);
            Spawn(pawnInstance, Owner); // Spawn on server

            _pawn.Value = pawnInstance.GetComponent<Pawn>();

            TargetPawnSpawned(Owner);
        }

        public void StopGame()
        {
            if (_pawn.Value != null && _pawn.Value.IsSpawned)
            {
                _pawn.Value.Despawn();
            }
        }

        [ServerRpc(RequireOwnership = false)]
        public void ServerSetIsReady(bool value)
        {
            IsReady = value;
        }

        [TargetRpc]
        private void TargetPawnSpawned(NetworkConnection networkConnection = null)
        {
            ViewManager.Instance.Show<UI.MainView>();
        }
    }
}
