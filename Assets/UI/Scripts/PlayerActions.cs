using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "NewPlayerActions", menuName = "PlayerActions")]
    public class PlayerActions : ScriptableObject
    {
        [Header("Units")]
        public List<Units.UnitBasic> basicUnits = new List<Units.UnitBasic>();

        [Space(5)]
        [Header("Buildings")]
        public List<Buildings.BuildingBasic> basicBuildings = new List<Buildings.BuildingBasic>();
    }
}
