using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Interactables;


namespace UI
{
    public class UnitUI : Interactable
    {
        [SerializeField]
        private GameObject gameController;
        private void Awake()
        {
            gameController = GameObject.Find("GameController");
            //Debug.Log(inputHandler);
        }


        public override void SetSelectedVisible(bool visible)
        {
            //foreach()
            if (ActionFrame.instance != null)
            {
                if (visible) ActionFrame.instance.SetActionButtonsBuilding(
                    gameObject.GetComponent<Units.Player.PlayerRTS>().baseStats.actions, 
                    gameObject
                    );
                else if (!visible) ActionFrame.instance.ClearActions();
            }
            Debug.Log(gameController);
            if (StatisticsFrame.instance != null)
            {
                
                if (visible && gameController.GetComponent<InputManager.InputHandler>().selectedUnitRTSList.Count==1) 
                    StatisticsFrame.instance.ChangeStatsOfObject(
                        gameObject.transform.Find("StatsDisplay").GetComponent<Core.HealthHandler>().currentHealth,
                        gameObject.transform.Find("StatsDisplay").GetComponent<Core.HealthHandler>().baseHealth,
                        gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite
                    );
                else if (!visible || gameController.GetComponent<InputManager.InputHandler>().selectedUnitRTSList.Count>1) StatisticsFrame.instance.Clear();
            }
            base.SetSelectedVisible(visible);
        }

        public override void SetSelection(bool visible)
        {
            base.SetSelection(visible);
        }
    }
}

