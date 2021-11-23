using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStats
{
    public class Statistics : MonoBehaviour
    {
        public Transform UnitsObject;
        internal int money = 500;
        internal int units;

        private void Awake()
        {
            if(UnitsObject==null) UnitsObject = GameObject.Find("PlayerUnits").transform;
            GetNumberOfUnits();
        }
        private void Start()
        {
            StartCoroutine(GetMoneyPassive());
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                money += 500;
            }
            GetNumberOfUnits();
            
        }

        public void GetNumberOfUnits()
        {
            units = 0;
            foreach (Transform child in UnitsObject)
            {
                foreach (Transform transformObject in child)
                {
                    units++;
                }
            }
        }

        public IEnumerator GetMoneyPassive()
        {
            yield return new WaitForSeconds(2);
            money += 10;
            StartCoroutine(GetMoneyPassive());
        }
    }
}

