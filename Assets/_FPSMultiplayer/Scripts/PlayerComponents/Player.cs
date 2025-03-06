using FishNet.CodeGenerating;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public sealed class Player : NetworkBehaviour
{
    [AllowMutableSyncType, SerializeField] private SyncVar<string> _userName = new SyncVar<string>();
    [AllowMutableSyncType, SerializeField] private SyncVar<bool> _isReady = new SyncVar<bool>();

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
}
