using System;
using FishNet;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public sealed class MultiplayerView : View
    {
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _connectButton;
        [SerializeField] private Button _exitButton;


        public override void Initialize()
        {
            _hostButton.onClick.AddListener(OnHostButtonClicked);
            _connectButton.onClick.AddListener(OnConnectButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);

            base.Initialize();
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }

        private void OnConnectButtonClicked()
        {
            InstanceFinder.ClientManager.StartConnection();
        }

        private void OnHostButtonClicked()
        {
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
        }
    }
}
