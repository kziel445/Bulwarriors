using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Statistics
{
    public class Statistics : MonoBehaviour
    {
        public float dataUpdateTime = 10;
        public Transform unitsObject;
        public Data dataObject;
        [SerializeField] internal int money = 100;
        [SerializeField] internal int units;
        
        private void Awake()
        {
            if (unitsObject == null)
            {
                if (gameObject.name.Contains("Player"))
                {
                    unitsObject = GameObject.Find("PlayerUnits").transform;
                    dataObject = GameObject.Find("PlayerData").transform.GetComponent<Data>();
                }
                    
                else if (gameObject.name.Contains("Enemy"))
                {
                    unitsObject = GameObject.Find("EnemyUnits").transform;
                    dataObject = GameObject.Find("EnemyData").transform.GetComponent<Data>();
                }
                    
                else Debug.LogWarning("Player or enemy statistics not found");
            }
            units = GetNumberOfUnits();
            dataObject.unitsRecruted = units;
            dataObject.moneyCollected = money;
            StartCoroutine(GetNewData());
        }
        private void Update()
        {
            dataObject.timer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.M))
            {
                GetMoney(500);
            }
            units = GetNumberOfUnits();
        }
        public void GetMoney(int amout)
        {
            money += amout;
            dataObject.moneyCollected += amout;
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
        public void UnitsRecrutedUpdate()
        {
            dataObject.unitsRecruted+=1;
        }
        public string TimerString()
        {
            return $"{Mathf.FloorToInt(dataObject.timer/60)}:{Mathf.FloorToInt(dataObject.timer % 60).ToString("00")}".ToString();
        }
        IEnumerator GetNewData()
        {
            dataObject.GetComponent<Data>().datas.Add(new DataRecord(dataObject.timer, money, units));
            yield return new WaitForSeconds(dataUpdateTime);
            StartCoroutine(GetNewData());
        }
    }
}

