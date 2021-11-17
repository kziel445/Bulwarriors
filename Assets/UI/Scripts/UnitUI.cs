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
            base.SetSelectedVisible(visible);
        }

        public override void SetSelection(bool visible)
        {
            base.SetSelection(visible);
        }
    }
}

