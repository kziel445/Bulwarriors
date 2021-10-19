using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Units
{
    public class UnitHandler : MonoBehaviour
    {
        public Unit _unit;
        public Transform warriors;
        void Start()
        {
            //GameObject unit = Instantiate(_unit.unitPrefab, transform.position, Quaternion.identity, warriors);
        }

        void Update()
        {

        }
    }
}