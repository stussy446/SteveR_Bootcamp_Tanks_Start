using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tanks
{
    public class LobbyListController : MonoBehaviour
    {
        [SerializeField] private Button createNewLobbyButton;
        [SerializeField] private Button joinPrivateLobbyButton;
        [SerializeField] private Button closeButton;

        [SerializeField] private LobbyListEntry lobbyListEntryPrefab;
        [SerializeField] private RectTransform entriesHolder;

        [SerializeField] private GameObject createLobbyPopup;
        [SerializeField] private GameObject joinPrivateLobbyPopup;

        private Dictionary<string, LobbyListEntry> entries;

        private void OnNewLobbyButtonClicked()
        {
            createLobbyPopup.SetActive(true);
        }

        private void OnJoinPrivateLobbyButtonClicked()
        {
            joinPrivateLobbyPopup.SetActive(true);
        }

        private void OnCloseButtonClicked()
        {
            SceneManager.LoadScene("MainMenu");
        }

        // TODO: Create, Update and Remove room entries

        private void Start()
        {
            LoadingGraphics.Disable();

            entries = new Dictionary<string, LobbyListEntry>();

            closeButton.onClick.AddListener(OnCloseButtonClicked);
            createNewLobbyButton.onClick.AddListener(OnNewLobbyButtonClicked);
            joinPrivateLobbyButton.onClick.AddListener(OnJoinPrivateLobbyButtonClicked);

            DestroyHolderChildren();

            createLobbyPopup.SetActive(false);
            joinPrivateLobbyPopup.SetActive(false);
        }

        private void DestroyHolderChildren()
        {
            for (var i = entriesHolder.childCount - 1; i >= 0; i--) {
                Destroy(entriesHolder.GetChild(i).gameObject);
            }
        }
    }
}