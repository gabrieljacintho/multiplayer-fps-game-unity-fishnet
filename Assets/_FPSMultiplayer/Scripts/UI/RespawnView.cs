using UnityEngine;
using UnityEngine.UI;

namespace FPSMultiplayer.UI
{
    public sealed class RespawnView : View
    {
        [SerializeField] private Button _respawnButton;


        public override void Initialize()
        {
            _respawnButton.onClick.AddListener(RespawnButtonClicked);
            base.Initialize();
        }

        private void RespawnButtonClicked()
        {
            Player.Instance.ServerSpawnPawn();
        }
    }
}