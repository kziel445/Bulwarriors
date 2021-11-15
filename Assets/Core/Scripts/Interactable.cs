using UnityEngine;

namespace Core.Interactables
{
    public class Interactable : MonoBehaviour
    {
        internal GameObject selectedGameObject;
        public bool isInteracting = false;
        private void Awake()
        {
            selectedGameObject = transform.Find("Selected").gameObject;
            SetSelectedVisible(false);
        }
        public virtual void SetSelection(bool visible)
        {
            selectedGameObject.SetActive(visible);
            isInteracting = visible;
        }
        public virtual void SetSelectedVisible(bool visible)
        {
            selectedGameObject.SetActive(visible);
        }

    }
}

