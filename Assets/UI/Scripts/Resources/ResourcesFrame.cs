using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourcesFrame : MonoBehaviour
    {
        public PlayerStats.Statistics statistics;
        // Start is called before the first frame update
        void Start()
        {
            statistics = GameObject.Find("PlayerStatistics").GetComponent<PlayerStats.Statistics>();
        }

        // Update is called once per frame
        void Update()
        {
            gameObject.transform.GetChild(0).GetComponentInChildren<Text>().text = statistics.money.ToString();
            gameObject.transform.GetChild(1).GetComponentInChildren<Text>().text = statistics.units.ToString();
        }
    }
}

