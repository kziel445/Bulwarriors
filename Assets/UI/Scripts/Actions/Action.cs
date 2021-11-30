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

        public bool isUnit=false;
        void Start()
        {

        }
        public void OnClick()
        {
            if(isUnit) ActionFrame.instance.spawnBuilding.GetComponent<ObjectSpawnQueue>().StartQueueTimer(name);
            if (!isUnit) PlayerBuilder.instance.SpawnScheme(name);
            //spawnQueue.StartQueueTimer(name);
            //UnitSpawnQueue.instance.StartQueueTimer(name);
        }


    }
}
