using UnityEngine;

namespace Tanks
{
    public class UIDirectionControl : MonoBehaviour
    {
        public bool m_UseRelativeRotation = true;
        private Quaternion relativeRotation;

        private void Start ()
        {
            relativeRotation = transform.parent.localRotation;
        }

        private void Update ()
        {
            if (m_UseRelativeRotation)
                transform.rotation = relativeRotation;
        }
    }
}