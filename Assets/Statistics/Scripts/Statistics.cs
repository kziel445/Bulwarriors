using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Statistics
{
    public class Statistics : MonoBehaviour
    {
        public Transform unitsObject;
        [SerializeField] internal int money = 100;
        [SerializeField] internal int moneyCollected;
        [SerializeField] internal int units;

        private void Awake()
        {
            moneyCollected = money;
            if (unitsObject == null)
            {
                if (gameObject.name.Contains("Player"))
                    unitsObject = GameObject.Find("PlayerUnits").transform;
                else if (gameObject.name.Contains("Enemy"))
                    unitsObject = GameObject.Find("EnemyUnits").transform;
                else Debug.LogWarning("Player or enemy statistics not found");
            }
            units = GetNumberOfUnits();
        }
        private void Start()
        {
            StartCoroutine(GetMoneyPassive(10));
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                GetMoney(500);
            }
            units = GetNumberOfUnits();

        }
        public void GetMoney(int amout)
        {
            money += amout;
            moneyCollected += amout;
        }
        public int GetNumberOfUnits()
        {
            var count = 0;
            foreach (Transform child in unitsObject)
            {
                count += child.childCount;
            }
            return count;
        }

        public IEnumerator GetMoneyPassive(int gold)
        {
            yield return new WaitForSeconds(2);
            GetMoney(gold);
            StartCoroutine(GetMoneyPassive(gold));
        }
    }
}

