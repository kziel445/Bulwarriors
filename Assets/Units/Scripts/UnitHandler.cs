using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Units
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;
        [SerializeField]
        public UnitBasic worker, warrior, archer, advancedWorker,
            shooter, knight, swordsman, ranger, scout;
        private void Awake()
        {
           instance = this;
        }
        public UnitBasic GetUnitSettings(string type)
        {
            UnitBasic unit;
            switch (type)
            {
                case "worker":
                    unit = worker;
                    break;
                case "warrior":
                    unit = warrior;
                    break;
                case "archer":
                    unit = archer;
                    break;
                case "advancedworker":
                    unit = advancedWorker;
                    break;
                case "shooter":
                    unit = shooter;
                    break;
                case "knight":
                    unit = knight;
                    break;
                case "swordsman":
                    unit = swordsman;
                    break;
                case "ranger":
                    unit = ranger;
                    break;
                case "scout":
                    unit = scout;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} could not be found or does not exist!");
                    return null;
            }
            return unit;
        }

    }
}