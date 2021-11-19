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
        
        void Start()
        {
        }
        public void OnClick()
        {
            
            ActionFrame.instance.spawnBuilding.GetComponent<SpawnObject>().StartQueueTimer(name);
            //spawnQueue.StartQueueTimer(name);
            //UnitSpawnQueue.instance.StartQueueTimer(name);
        }

    }
}
