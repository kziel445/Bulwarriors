using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
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

        // Update is called once per frame
        void Update()
        {

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

