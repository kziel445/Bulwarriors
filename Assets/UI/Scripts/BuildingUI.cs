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
            if(ActionFrame.instance!=null)
            {
                if (visible) ActionFrame.instance.SetActionButtons(actions, gameObject.transform);
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
