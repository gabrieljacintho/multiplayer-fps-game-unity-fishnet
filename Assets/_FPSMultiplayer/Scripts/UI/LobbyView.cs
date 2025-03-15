using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FPSMultiplayer
{
    public sealed class LobbyView : View
    {
        [SerializeField] private Button _toggleReadyButton;
        [SerializeField] private TextMeshProUGUI _toggleReadyButtonText;
        [SerializeField] private Button _startGameButton;


        public override void Initialize()
        {
            base.Initialize();

            _toggleReadyButton.onClick.AddListener(OnToggleReadyButtonClicked);

            _startGameButton.gameObject.SetActive(IsHostInitialized);

            if (IsHostInitialized)
            {
                _startGameButton.onClick.AddListener(OnStartGameButtonClicked);
            }
        }

        private void Update()
        {
            if (!IsInitialized)
            {
                return;
            }

            _toggleReadyButtonText.color = Player.Instance.IsReady ? Color.green : Color.red;

            _startGameButton.interactable = GameManager.Instance.CanStart.Value;
        }

        private void OnToggleReadyButtonClicked()
        {
            Player.Instance.ServerSetIsReady(!Player.Instance.IsReady);
        }

        private void OnStartGameButtonClicked()
        {
            GameManager.Instance.StartGame();
        }
    }
}
