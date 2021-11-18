using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActionFrame : MonoBehaviour
    {
        //Checked
        public static ActionFrame instance;
        [SerializeField] private Button actionButton;
        [SerializeField] private Transform actionListUI;


        private PlayerActions actionList = null;
        public GameObject spawnBuilding = null;
        public List<Button> buttons = new List<Button>();
        //

        
        

        //public List<float> spawningQueueTimer = new List<float>();
        //public List<GameObject> spawnQueue = new List<GameObject>();
        //public List<Units.UnitBasic.unitType> spawnTypes = new List<Units.UnitBasic.unitType>();

        //public Transform objectToStoreUnits;



        private void Awake()
        {
            instance = this;
        }
        
        //clear buttons from UI
        public void ClearActions()
        {
            Debug.Log("Clear buttons");
            foreach(Button button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
        //worker actions


        //building actions
        public void SetActionButtonsBuilding(PlayerActions actions, GameObject spawnLocation)
        {
            //unused
            spawnBuilding = spawnLocation;
            actionList = actions;
            if (actions.basicUnits.Count > 0)
            {
                foreach (Units.UnitBasic unit in actions.basicUnits)
                {
                    Button button = Instantiate(actionButton, actionListUI);
                    button.name = unit.name;
                    button.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = unit.icon;
                    buttons.Add(button);
                }
            }
            if (actions.basicBuildings.Count > 0)
            {
                foreach (Buildings.BuildingBasic building in actions.basicBuildings)
                {
                    Button button = Instantiate(actionButton, actionListUI);
                    button.name = building.name;
                    buttons.Add(button);
                }
            }
        }
    }

}