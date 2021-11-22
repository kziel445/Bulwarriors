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

            base.SetSelectedVisible(visible);
        }

        public override void SetSelection(bool visible)
        {
            base.SetSelection(visible);
        }
    }
}

