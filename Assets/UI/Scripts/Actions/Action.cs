using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buildings;

//TODO reformat code
namespace UI
{
    public class Action : MonoBehaviour
    {
        public static Action instance;
        public void OnClick()
        {
            ActionFrame.instance.spawnBuilding.GetComponent<UnitSpawnQueue>().StartQueueTimer(name);
            //spawnQueue.StartQueueTimer(name);
            //UnitSpawnQueue.instance.StartQueueTimer(name);
        }

    }
}