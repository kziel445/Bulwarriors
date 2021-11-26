using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Buildings
{
    public class GoldUp : MonoBehaviour
    {
        PlayerStats.Statistics statistics;
        [SerializeField]
        private int goldPerTime = 10;
        [SerializeField]
        private int secondsToGold = 2;

        private void Awake()
        {
            statistics = GameObject.Find("PlayerStatistics").GetComponent<PlayerStats.Statistics>();
        }
        private void Start()
        {
            StartCoroutine(GetMoneyPassive(goldPerTime));
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator GetMoneyPassive(int gold)
        {
            yield return new WaitForSeconds(secondsToGold);
            statistics.GetMoney(gold);
            StartCoroutine(GetMoneyPassive(gold));
        }
    }
}

