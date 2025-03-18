using FishNet.CodeGenerating;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace BoardGame
{
    public sealed class Player : NetworkBehaviour
    {
        public static Player Instance { get; private set; }

        [AllowMutableSyncType, SerializeField] private SyncVar<string> _username = new SyncVar<string>();
        [AllowMutableSyncType, SerializeField] private SyncVar<bool> _isReady = new SyncVar<bool>();
        [AllowMutableSyncType, SerializeField] private SyncVar<Pawn> _pawn = new SyncVar<Pawn>();


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
        }
    }
}
