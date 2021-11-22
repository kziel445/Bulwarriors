using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Interactables;


namespace UI
{
    public class UnitUI : Interactable
    {
        public override void SetSelectedVisible(bool visible)
        {
            //foreach()
            if (ActionFrame.instance != null)
            {
                Debug.Log("visible" + visible);
                if (visible) ActionFrame.instance.SetActionButtonsBuilding(
                    gameObject.GetComponent<Units.Player.PlayerRTS>().baseStats.actions, 
                    gameObject
                    );
                else if (!visible) ActionFrame.instance.ClearActions();
            }
            //if(StatisticsFrame.instance!=null)
            //{
            //    if (visible) StatisticsFrame.instance.ChangeStatsOfObject(
            //        gameObject.transform.Find("StatsDisplay").GetComponent<Core.HealthHandler>().currentHealth,
            //        gameObject.transform.Find("StatsDisplay").GetComponent<Core.HealthHandler>().baseHealth,
            //        gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite
            //        );
            //    else if (!visible) StatisticsFrame.instance.Clear();
            //}
            base.SetSelectedVisible(visible);
        }

        public override void SetSelection(bool visible)
        {
            base.SetSelection(visible);
        }
    }
}

