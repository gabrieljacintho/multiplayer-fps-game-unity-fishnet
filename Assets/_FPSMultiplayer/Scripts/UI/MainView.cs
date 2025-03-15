using TMPro;
using UnityEngine;

namespace FPSMultiplayer.UI
{
    public sealed class MainView : View
    {
        [SerializeField] private TextMeshProUGUI _healthText;


        private void Update()
        {
            if (!IsInitialized)
            {
                return;
            }

            Player player = Player.Instance;
            if (player == null || player.Pawn == null)
            {
                return;
            }

            _healthText.text = $"Health: {player.Pawn.Health}";
        }
    }
}