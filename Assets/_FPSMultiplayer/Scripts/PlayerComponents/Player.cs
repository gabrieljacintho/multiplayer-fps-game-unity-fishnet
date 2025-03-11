using System;
using FishNet.CodeGenerating;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.AddressableAssets;

public sealed class Player : NetworkBehaviour
{
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

        if (Input.GetKeyDown(KeyCode.I))
        {
            ServerSpawnPawn();
        }
    }

    [ServerRpc]
    public void ServerSetIsReady(bool value)
    {
        IsReady = value;
    }

    [ServerRpc]
    private void ServerSpawnPawn()
    {
        GameObject pawnPrefab = Addressables.LoadAssetAsync<GameObject>("Pawn").WaitForCompletion(); // Not async

        GameObject pawnInstance = Instantiate(pawnPrefab);
        Spawn(pawnInstance, Owner); // Spawn on server
    }
}
