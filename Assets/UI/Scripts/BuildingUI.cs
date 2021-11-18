using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Interactables;

namespace UI
{
    public class BuildingUI : Interactable
    {

        public override void SetSelectedVisible(bool visible)
        {
            if(ActionFrame.instance!=null)
            {
                if (visible) ActionFrame.instance.SetActionButtonsBuilding(
                    gameObject.GetComponent<Buildings.Player.PlayerBuilding>().baseStats.actions, 
                    gameObject.transform
                    );
                else if (!visible) ActionFrame.instance.ClearActions();
            }
            if(StatisticsFrame.instance!=null)
            {
                if (visible) StatisticsFrame.instance.ChangeStatsOfObject(
                    gameObject.transform.Find("StatsDisplay").GetComponent<Core.HealthHandler>().currentHealth,
                    gameObject.transform.Find("StatsDisplay").GetComponent<Core.HealthHandler>().baseHealth
                    );
                else if (!visible) Debug.Log("disabledStats");//ActionFrame.instance.ClearActions();
            }
            base.SetSelectedVisible(visible);
        }

        public override void SetSelection(bool visible)
        {
            
            base.SetSelection(visible);
        }
    }
}
