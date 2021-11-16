using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Interactables;

namespace UI
{
    public class BuildingUI : Interactable
    {
        public PlayerActions actions;

        public override void SetSelectedVisible(bool visible)
        {
            base.SetSelectedVisible(visible);
            ActionFrame.instance.SetActionButtons(actions);
            Debug.Log("Visible!");
            
        }

        public override void SetSelection(bool visible)
        {
            ActionFrame.instance.ClearActions();
            base.SetSelection(visible);
        }
    }
}
