using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class BuildingRTS : MonoBehaviour, IClickable
    {
        public BuildingStatTypes.Base baseStats;
        [System.Serializable]
        public class BuildUnits
        {
            public Units.UnitBasic[] basicUnits;

        }
        public void Click()
        {
            Debug.Log("Building options in UI");
            Debug.Log("Building health");
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
