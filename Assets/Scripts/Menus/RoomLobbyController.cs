using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tanks
{
    public class RoomLobbyController : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button closeButton;

        [SerializeField] private PlayerLobbyEntry playerLobbyEntryPrefab;
        [SerializeField] private RectTransform entriesHolder;

        // TODO: Create and Delete player entries

        private void AddLobbyEntry()
        {
            var entry = Instantiate(playerLobbyEntryPrefab, entriesHolder);
            entry.Setup();

            // TODO: track created player lobby entries
        }

        private void Start()
        {
            LoadingGraphics.Disable();
            DestroyHolderChildren();

            closeButton.onClick.AddListener(OnCloseButtonClicked);
            startButton.onClick.AddListener(OnStartButtonClicked);
            startButton.gameObject.SetActive(false);
        }

        private void UpdateStartButton()
        {
            // TODO: Show start button only to the master client and when all players are ready
        }

        private void OnStartButtonClicked()
        {
            // TODO: Load gameplay level for all clients
        }

        private void OnCloseButtonClicked()
        {
            // TODO: Leave room
            SceneManager.LoadScene("MainMenu");
        }

        private void DestroyHolderChildren()
        {
            for (var i = entriesHolder.childCount - 1; i >= 0; i--) {
                Destroy(entriesHolder.GetChild(i).gameObject);
            }
        }
    }
}