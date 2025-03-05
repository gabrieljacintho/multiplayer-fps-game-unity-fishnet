using FishNet.CodeGenerating;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public sealed class Player : NetworkBehaviour
{
    [AllowMutableSyncType, SerializeField] private SyncVar<int> _score = new SyncVar<int>();

    private readonly SyncVar<int> _playerCount = new SyncVar<int>();

    public static Player Instance { get; private set; }
    public int Score
    {
        get => _score.Value;
        [ServerRpc]
        private set => _score.Value = value;
    }
    public int PlayerCount => _playerCount.Value;


    private void Update()
    {
        if (IsServerInitialized)
        {
            _playerCount.Value = ServerManager.Clients.Count;
        }

        if (!IsOwner)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Score = Random.Range(0, 1024);
        }
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
    }
}
