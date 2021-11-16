using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActionFrame : MonoBehaviour
    {
        public static ActionFrame instance;
        [SerializeField] private Button actionButton;
        [SerializeField] private Transform actionListUI;

        public List<Button> buttons = new List<Button>();

        private void Awake()
        {
            instance = this;
        }
        public void SetActionButtons(PlayerActions actions)
        {
            if(actions.basicUnits.Length>0)
            {
                foreach(Units.UnitBasic unit in actions.basicUnits)
                {
                    Button button = Instantiate(actionButton, actionListUI);
                    button.name = unit.name;
                    buttons.Add(button);
                }
            }
            if(actions.basicBuildings.Length>0)
            {
                foreach(Buildings.BuildingBasic building in actions.basicBuildings)
                {
                    Button button = Instantiate(actionButton, actionListUI);
                    button.name = building.name;
                    buttons.Add(button);
                }
            }
        }
        public void ClearActions()
        {
            Debug.Log("Clear buttons");
            foreach(Button button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
    }

}