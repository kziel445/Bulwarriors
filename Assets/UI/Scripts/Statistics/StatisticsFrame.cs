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
            gameObject.GetComponentInChildren<Text>().text= $"Health: {currentHealth} \nMax health: {maxHealth}";
        }
        public void Clear()
        {

            //better would be visible off
            Debug.Log("Clear Stats");
            gameObject.GetComponentInChildren<Text>().text = "";
            gameObject.transform.Find("Icon").GetComponent<Image>().sprite = null; ;
            //foreach (Transform child in transform)
            //{
            //    Destroy(child.gameObject);
            //}
        }
    }
}
