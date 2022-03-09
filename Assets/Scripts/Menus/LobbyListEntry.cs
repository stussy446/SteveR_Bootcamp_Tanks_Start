using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks
{
    public class LobbyListEntry : MonoBehaviour
    {
        [SerializeField] private Button enterButton;
        [SerializeField] private TMP_Text lobbyNameText;
        [SerializeField] private TMP_Text lobbyPlayerCountText;

        private void OnEnterButtonClick()
        {
            LoadingGraphics.Enable();

            // TODO: Join target room
        }

        public void Setup()
        {
            // TODO: Store and update room information
        }

        private void Start()
        {
            enterButton.onClick.AddListener(OnEnterButtonClick);
        }
    }
}