using FishNet.CodeGenerating;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace BoardGame
{
    public sealed class Pawn : NetworkBehaviour
    {
        [AllowMutableSyncType, SerializeField] private SyncVar<Player> _player = new SyncVar<Player>();
    }
}
