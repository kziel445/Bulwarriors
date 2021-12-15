using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class GoldUp : MonoBehaviour
    {
        Statistics.Statistics statistics;
        [SerializeField] private int goldPerTime = 10;
        [SerializeField] private int secondsToGold = 2;

        private void Awake()
        {
            if (gameObject.name.Contains("Player"))
                statistics = GameObject.Find("PlayerStatistics").GetComponent<Statistics.Statistics>();
            else if (gameObject.name.Contains("Enemy"))
                statistics = GameObject.Find("EnemyStatistics").GetComponent<Statistics.Statistics>();
            else Debug.LogWarning("Player or enemy statistics not found");
            
        }
        private void Start()
        {
            StartCoroutine(GetMoneyPassive(goldPerTime));
        }

        public IEnumerator GetMoneyPassive(int gold)
        {
            yield return new WaitForSeconds(secondsToGold);
            statistics.GetMoney(gold);
            StartCoroutine(GetMoneyPassive(gold));
        }
    }
}

