using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO reformat code
namespace UI
{
    public class Action : MonoBehaviour
    {
        public static Action instance;
        public void OnClick()
        {
            ActionFrame.instance.StartQueueTimer(name);
        }

    }
}
