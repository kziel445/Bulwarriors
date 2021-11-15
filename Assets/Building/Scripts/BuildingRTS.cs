using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class BuildingRTS : MonoBehaviour, IClickable
    {
        //statistics
        public BuildingStatTypes.Base baseStats;
        public Image healthBarAmount;
        public float currentHealth;

        internal GameObject selectedGameObject;

        [System.Serializable]
        public class BuildUnits
        {
            public Units.UnitBasic[] basicUnits;

        }
        public void Click()
        {

            Debug.Log("Building options in UI");
            Debug.Log("Building health");
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SetSelectedVisible(bool visible)
        {
            selectedGameObject.SetActive(visible);
        }

    }

}
