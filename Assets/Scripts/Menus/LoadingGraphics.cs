using UnityEngine;

namespace Tanks
{
    public class LoadingGraphics : MonoBehaviour
    {
        private static LoadingGraphics instance;

        public static void Enable()
        {
            instance.gameObject.SetActive(true);
        }

        public static void Disable()
        {
            instance.gameObject.SetActive(false);
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            instance = this;
            Disable();
        }
    }
}