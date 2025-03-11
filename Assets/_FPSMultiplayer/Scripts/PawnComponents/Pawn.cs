using FishNet.CodeGenerating;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public sealed class Pawn : NetworkBehaviour
{
    [AllowMutableSyncType, SerializeField] private SyncVar<Player> _player = new SyncVar<Player>();
    [AllowMutableSyncType, SerializeField] private SyncVar<float> _health = new SyncVar<float>();
}
