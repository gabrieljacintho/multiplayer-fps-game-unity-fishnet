using System;
using FishNet;
using UnityEngine;
using UnityEngine.UI;

namespace FPSMultiplayer
{
    public class MultiplayerMenu : MonoBehaviour
    {
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _connectButton;


        private void Start()
        {
            _hostButton.onClick.AddListener(OnHostButtonClick);
            _connectButton.onClick.AddListener(OnConnectButtonClick);
        }

        private void OnHostButtonClick()
        {
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
        }

        private void OnConnectButtonClick()
        {
            InstanceFinder.ClientManager.StartConnection();
        }
    }
}
