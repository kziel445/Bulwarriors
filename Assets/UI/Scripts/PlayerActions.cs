using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "NewPlayerActions", menuName = "PlayerActions")]
    public class PlayerActions : ScriptableObject
    {
        [Header("Units")]
        public Units.UnitBasic[] basicUnits = new Units.UnitBasic[0];

        [Space(5)]
        [Header("Buildings")]
        public Buildings.BuildingBasic[] basicBuildings = new Buildings.BuildingBasic[0];
    }
}
