using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Buildings
{
    public class GoldUp : MonoBehaviour
    {
        PlayerStats.Statistics statistics;
        private void Awake()
        {
            statistics = GameObject.Find("PlayerStatistics").GetComponent<PlayerStats.Statistics>();
        }
        private void Start()
        {
            StartCoroutine(GetMoneyPassive(5));
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator GetMoneyPassive(int gold)
        {
            yield return new WaitForSeconds(2);
            statistics.money += gold;
            StartCoroutine(GetMoneyPassive(gold));
        }
    }
}

