using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourcesFrame : MonoBehaviour
    {
        public PlayerStats.Statistics statistics;
        void Start()
        {
            statistics = GameObject.Find("PlayerStatistics").GetComponent<PlayerStats.Statistics>();
        }

        void Update()
        {
            gameObject.transform.GetChild(0).GetComponentInChildren<TMPro.TextMeshProUGUI>().text = statistics.money.ToString();
            gameObject.transform.GetChild(1).GetComponentInChildren<TMPro.TextMeshProUGUI>().text = statistics.units.ToString();
        }
    }
}

