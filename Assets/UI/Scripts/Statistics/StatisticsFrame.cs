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
        private void Update()
        {
               
        }
        public void ChangeStatsOfObject(float currentHealth, float maxHealth, Sprite sprite)
        {
            gameObject.transform.Find("Icon").GetComponent<Image>().sprite = sprite;
            gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text= $"Health: {currentHealth} \nMax health: {maxHealth}";
        }
        public void Clear()
        {
            Debug.Log("Clear Stats");
            gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
            gameObject.transform.Find("Icon").GetComponent<Image>().sprite = null; ;

        }
    }
}
