using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStats
{
    public class Statistics : MonoBehaviour
    {
        public Transform UnitsObject;
        internal int money = 100;
        internal int moneyCollected;
        internal int units;

        private void Awake()
        {
            moneyCollected = money;
            if(UnitsObject==null) UnitsObject = GameObject.Find("PlayerUnits").transform;
            GetNumberOfUnits();
        }
        private void Start()
        {
            StartCoroutine(GetMoneyPassive(10));
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                GetMoney(500);
            }
            GetNumberOfUnits();
            
        }

        public void GetMoney(int amout)
        {
            money += amout;
            moneyCollected += amout;
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

        public IEnumerator GetMoneyPassive(int gold)
        {
            yield return new WaitForSeconds(2);
            GetMoney(gold);
            StartCoroutine(GetMoneyPassive(gold));
        }
    }
}

