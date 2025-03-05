using System;
using FishNet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainView : View
{
    [SerializeField] private Button _disconnectButton;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _playerCountText;


    public override void Initialize()
    {
        _disconnectButton.onClick.AddListener(OnDisconnectButtonClicked);

        base.Initialize();
    }

    private void LateUpdate()
    {
        if (!IsInitialized)
        {
            return;
        }

        _infoText.text =
            $"Is Server: {InstanceFinder.IsServerStarted}\n" +
            $"Is Client: {InstanceFinder.IsClientStarted}\n" +
            $"Is Host: {InstanceFinder.IsHostStarted}";

        _scoreText.text = $"Score: {Player.Instance.Score} Points";

        if (Player.Instance.IsHostInitialized)
        {
            _playerCountText.text = $"Player Count: {Player.Instance.PlayerCount}";
            _playerCountText.gameObject.SetActive(true);
        }
        else
        {
            _playerCountText.gameObject.SetActive(false);
        }
    }

    private void OnDisconnectButtonClicked()
    {
        if (InstanceFinder.IsServerStarted)
        {
            InstanceFinder.ServerManager.StopConnection(true);
        }
        else if (InstanceFinder.IsClientStarted)
        {
            InstanceFinder.ClientManager.StopConnection();
        }
    }
}
