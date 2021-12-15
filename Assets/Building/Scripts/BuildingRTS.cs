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
        public bool isBuilded = true;

        internal GameObject selectedGameObject;

        private Core.HealthHandler healthHandler;
        [System.Serializable]
        public class BuildUnits
        {
            public Units.UnitBasic[] basicUnits;
        }
        private void Start()
        {
            try 
            {
                healthHandler = gameObject.GetComponentInChildren<Core.HealthHandler>();
            }
            catch
            {
                Debug.LogWarning("HealtHandler not loaded");
            }
        }
        private void Update()
        {
            
            if (!isBuilded && healthHandler.baseHealth == healthHandler.currentHealth)
            {
                TurnOnOffFunctions(true);
                Debug.Log(healthHandler.baseHealth + " ¿ycia  " + healthHandler.currentHealth);
            }
                
        }
        public void Click()
        {
            Debug.Log("Building options in UI");
            Debug.Log("Building health");
        }
        public void SetSelectedVisible(bool visible)
        {
            selectedGameObject.SetActive(visible);
        }
        public void TurnOnOffFunctions(bool onOff)
        {
            try
            {
                if (gameObject.GetComponent<GoldUp>() != null) gameObject.GetComponent<GoldUp>().enabled = onOff;
                isBuilded = onOff;
            }
            catch
            {
                Debug.LogWarning("Not disabled scripts");
            }
            

        }
    }

}
