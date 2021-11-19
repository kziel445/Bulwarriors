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

        public bool ActiveUI = false;
        public PlayerStats.Statistics statistics;

        private PlayerActions actionList = null;
        public GameObject spawnBuilding = null;
        public List<Button> buttons = new List<Button>();


        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            statistics = GameObject.Find("PlayerStatistics").GetComponent<PlayerStats.Statistics>();
        }

        private void Update()
        {
            if(ActiveUI)
            {
                try
                {

                    foreach (Button button in buttons)
                    {

                        if (statistics.money < int.Parse(button.GetComponentInChildren<Text>().text))
                        {
                            button.interactable = false;
                            button.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
                            
                        }
                        else
                        {
                            button.interactable = true;
                            button.GetComponent<Image>().color = new Color(255, 255, 255, 0.4f);
                        }
                    }
                }
                catch { }
            }
        }

        //clear buttons from UI
        public void ClearActions()
        {
            Debug.Log("Clear buttons");
            foreach(Button button in buttons)
            {
                Destroy(button.gameObject);
            }
            ActiveUI = false;
            buttons.Clear();
        }
        //worker actions


        //building actions
        public void SetActionButtonsBuilding(PlayerActions actions, GameObject spawnLocation)
        {
            ActiveUI = true;
            if (actions == null) return;
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
                    button.gameObject.transform.GetChild(1).GetComponent<Text>().text = unit.baseStats.cost.ToString();
                    buttons.Add(button);
                }
            }
            if (actions.basicBuildings.Count > 0)
            {
                foreach (Buildings.BuildingBasic building in actions.basicBuildings)
                {
                    Button button = Instantiate(actionButton, actionListUI);
                    button.name = building.name;
                    button.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = building.icon;
                    button.gameObject.transform.GetChild(1).GetComponent<Text>().text = building.baseStats.cost.ToString();
                    buttons.Add(button);
                }
            }
        }
    }

}