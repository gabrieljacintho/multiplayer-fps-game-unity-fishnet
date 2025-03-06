using System;
using FishNet;
using UnityEngine;
using UnityEngine.UI;

public sealed class MultiplayerView : View
{
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _connectButton;
    [SerializeField] private Button _messageButton;


    public override void Initialize()
    {
        _hostButton.onClick.AddListener(OnHostButtonClicked);
        _connectButton.onClick.AddListener(OnConnectButtonClicked);
        _messageButton.onClick.AddListener(OnMessageButtonClicked);

        base.Initialize();
    }

    private void OnHostButtonClicked()
    {
        InstanceFinder.ServerManager.StartConnection();
        InstanceFinder.ClientManager.StartConnection();
    }

    private void OnConnectButtonClicked()
    {
        InstanceFinder.ClientManager.StartConnection();
    }

    private void OnMessageButtonClicked()
    {
        ViewManager.Instance.Show<MessageView>("Hello, World!");
    }
}
