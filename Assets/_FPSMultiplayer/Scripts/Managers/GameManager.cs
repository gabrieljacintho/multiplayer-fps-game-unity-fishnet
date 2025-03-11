using System.Linq;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

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
}
