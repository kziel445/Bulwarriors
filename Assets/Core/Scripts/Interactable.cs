using UnityEngine;

namespace Core.Interactables
{
    public class Interactable : MonoBehaviour
    {
        public static Interactable instance;
        [SerializeField] internal GameObject selectedGameObject;
        public bool isInteracting = false;
        private void Awake()
        {
            instance = this;
            //selectedGameObject = transform.Find("Selected").gameObject;

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

