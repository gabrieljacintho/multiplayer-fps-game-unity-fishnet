using System.Linq;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace FPSMultiplayer
{
    public sealed class GameManager : NetworkBehaviour
    {
        public readonly SyncList<Player> Players = new SyncList<Player>();
        public readonly SyncVar<bool> CanStart = new SyncVar<bool>();

        public static GameManager Instance { get; private set; }


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

            CanStart.Value = Players.All(p => p.IsReady);

            Debug.Log($"Can Start = {CanStart.Value}");
        }

        [Server]
        public void StartGame()
        {
            if (!CanStart.Value)
            {
                return;
            }

            for (int i = 0; i < Players.Count; i++)
            {
                Player player = Players[i];
                player.StartGame();
            }
        }

        [Server]
        public void StopGame()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Player player = Players[i];
                player.StopGame();
            }
        }
    }
}
