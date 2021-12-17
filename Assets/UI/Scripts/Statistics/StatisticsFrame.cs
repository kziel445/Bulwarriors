using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StatisticsFrame : MonoBehaviour
    {
        public static StatisticsFrame instance;
        public GameObject text;
        public GameObject icon;
        private void Awake()
        {
            instance = this;
        }
        public void ChangeStatsOfObject(float currentHealth, float maxHealth, Sprite sprite)
        {
            text.SetActive(true);
            icon.SetActive(true);
            text.GetComponent<TMPro.TextMeshProUGUI>().text = $"Health: {currentHealth} \nMax health: {maxHealth}";
            icon.GetComponent<Image>().sprite = sprite;
        }
        public void Clear()
        {
            text.GetComponent<TMPro.TextMeshProUGUI>().text = " ";
            icon.GetComponent<Image>().sprite = null; ;
            text.SetActive(false);
            icon.SetActive(false);
        }
    }
}
