using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks
{
    public class JoinPrivateLobbyPopup : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_InputField lobbyNameInput;
        [SerializeField] private Button enterButton;
        [SerializeField] private Button closeButton;

        private void Start()
        {
            enterButton.onClick.AddListener(OnEnterClicked);
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        public override void OnEnable()
        {
            lobbyNameInput.text = string.Empty;
            lobbyNameInput.Select();
            lobbyNameInput.ActivateInputField();

            base.OnEnable();
        }

        private void OnCloseButtonClicked()
        {
            gameObject.SetActive(false);
        }

        private void OnEnterClicked()
        {
            if (string.IsNullOrEmpty(lobbyNameInput.text)) return;
            LoadingGraphics.Enable();

            // Join target room
            PhotonNetwork.JoinRoom(lobbyNameInput.text);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log(message);
        }

    }
}