using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buildings;

namespace UI
{
    public class Action : MonoBehaviour
    {
        public static Action instance;

        public bool isUnit=false;

        public void OnClick()
        {
            if(isUnit) ActionFrame.instance.spawnBuilding.GetComponent<ObjectSpawnQueue>().StartQueueTimer(name);
            if (!isUnit) PlayerBuilder.instance.SpawnScheme(name);
        }
    }
}
