using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Statistics
{
    public class Graph : MonoBehaviour
    {
        [SerializeField] private Sprite circleSprite;
        private RectTransform graphContainer;
        [SerializeField] float unitsPointModifier = 50;
        //tmp
        Data dataSet;
        private void Awake() 
        {
            graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();

            //tmp data
            dataSet = new Data();
            dataSet.datas = new List<DataRecord>(){
                new DataRecord(0,100,1),
                new DataRecord(10,100,4),
                new DataRecord(20,100,4),
                new DataRecord(30,1020,4),
                new DataRecord(40,800,4),
                new DataRecord(50,700,7),
                new DataRecord(60,3300,7),
                new DataRecord(70,5200,8),
                new DataRecord(80,10000,40)
            };
            dataSet.moneyCollected = 40000;
            dataSet.timer = 100;
            dataSet.unitsRecruted = 40;
        }
        private void AddPoint(Vector2 anchoredPosition)
        {
            GameObject gameObject = new GameObject("circle", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().sprite = circleSprite;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = new Vector2(11,11);
            rectTransform.anchorMin = new Vector2(0,0);
            rectTransform.anchorMax = new Vector2(0,0);
        }
        private void ShowGraph(Dictionary<float,int> values)
        {
            float graphHeight = graphContainer.sizeDelta.y;
            float graphWidth = graphContainer.sizeDelta.x;
            float xSize = graphWidth/values.Count;
            float yMaximum = 0;
            foreach(KeyValuePair<float, int> datas in values)
            {
                if(datas.Value>yMaximum) yMaximum = datas.Value;
            }
            int i = 0;
            foreach(KeyValuePair<float, int> datas in values)
            {
                float xPosition =  i * xSize;
                float yPosition = (datas.Value / yMaximum) * graphHeight;
                AddPoint(new Vector2(xPosition, yPosition));
                i++;
            }
        }
        public void ShowUnitsGraph()
        {
            Dictionary<float, int> values = new Dictionary<float,int>();
            foreach(DataRecord record in dataSet.datas)
            {
                values.Add(record.time, record.units);
            }
            ShowGraph(values);
        }
        public void ShowMoneysGraph()
        {
            Dictionary<float, int> values = new Dictionary<float,int>();
            foreach(DataRecord record in dataSet.datas)
            {
                values.Add(record.time, record.money);
            }
            ShowGraph(values);
        }
        public void ShowPointsGraph()
        {
            Dictionary<float, int> values = new Dictionary<float,int>();
            foreach(DataRecord record in dataSet.datas)
            {
                values.Add(record.time, (int)Points(record.time, record.money, record. units));
            }
            ShowGraph(values);
        }
        public float Points(float time, int money, int units)
        {
            float points = (money + units * unitsPointModifier)/time;
            return points;
        }
    }
    
}
