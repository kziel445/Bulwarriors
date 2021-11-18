using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StatisticsFrame : MonoBehaviour
    {
        public static StatisticsFrame instance;
        private void Awake()
        {
            instance = this;
        }
        public void ChangeStatsOfObject(float currentHealth, float maxHealth)
        {
            gameObject.GetComponentInChildren<Text>().text= $"Health: {currentHealth}	Max health: {maxHealth}";
        }
        public void Clear()
        {

            //better would be visible off
            Debug.Log("Clear Stats");
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
