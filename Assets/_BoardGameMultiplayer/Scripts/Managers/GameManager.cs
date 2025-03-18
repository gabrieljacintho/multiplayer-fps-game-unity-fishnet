using FishNet.CodeGenerating;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace BoardGame
{
    public sealed class GameManager : NetworkBehaviour
    {
        public static GameManager Instance { get; private set; }

        [AllowMutableSyncType, SerializeField] private SyncVar<bool> _canStart = new SyncVar<bool>();
        [AllowMutableSyncType, SerializeField] private SyncVar<bool> _didStart = new SyncVar<bool>();

        public readonly SyncList<Player> Players = new SyncList<Player>();


        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (!IsServerInitialized)
            {
                return;
            }

            _canStart.Value = Players.Count > 1;

            Debug.Log($"There are {Players.Count} players in the game.");
        }

        [Server]
        public void StartGame()
        {
            if (!_canStart.Value)
            {
                return;
            }

            _didStart.Value = true;
        }

        [Server]
        public void StopGame()
        {
            _didStart.Value = false;
        }
    }
}
