using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Statistics
{
    public class Graph : MonoBehaviour
    {
        [SerializeField] private Sprite circleSprite;
        private Color color = Color.white;
        private RectTransform graphContainer;
        [SerializeField] private RectTransform labelTemplate;
        [SerializeField] float unitsPointModifier = 50;
        //tmp
        Data dataSet;
        private void Awake() 
        {
            graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
            //tmp data
            dataSet = new Data();
            dataSet.datas = new List<DataRecord>(){
                new DataRecord(0,100,4),
                new DataRecord(10,100,4),
                new DataRecord(20,100,4),
                new DataRecord(30,100,4),
                new DataRecord(40,100,4)
            };
            dataSet.moneyCollected = 40000;
            dataSet.timer = 100;
            dataSet.unitsRecruted = 40;
            if(GameObject.Find("PlayerData")!= null)
                dataSet = GameObject.Find("PlayerData").GetComponent<Data>();
        }
        private GameObject AddPoint(Vector2 anchoredPosition)
        {
            GameObject gameObject = new GameObject("circle", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().sprite = circleSprite;
            gameObject.GetComponent<Image>().color = color;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = new Vector2(11,11);
            rectTransform.anchorMin = new Vector2(0,0);
            rectTransform.anchorMax = new Vector2(0,0);

            return gameObject;
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
            yMaximum = MaximumY(yMaximum);
            
            int i = 0;
            GameObject lastPoint = null;
            foreach(KeyValuePair<float, int> datas in values)
            {
                float xPosition =  i * xSize;
                float yPosition = (datas.Value / yMaximum) * graphHeight;
                GameObject point = AddPoint(new Vector2(xPosition, yPosition));
                if(lastPoint != null) 
                    CreateLines(lastPoint.GetComponent<RectTransform>().anchoredPosition,
                        point.GetComponent<RectTransform>().anchoredPosition);
                lastPoint = point;

                i++;
            }
            
            int separatorCount = 10;
            for(int j = 0; j <= separatorCount; j++)
            {
                //TODO setParent to instainate
                RectTransform labelX = Instantiate(labelTemplate);
                labelX.SetParent(graphContainer);
                labelX.gameObject.SetActive(true);
                float normalizedValue = j * 1f/separatorCount;
                labelX.anchoredPosition = new Vector2(normalizedValue * graphWidth, -20f);
                float maxTimerValue = GameObject.Find("PlayerData").GetComponent<Data>().timer;
                labelX.GetComponent<TMPro.TextMeshProUGUI>().text = GameObject.Find("PlayerData").GetComponent<Data>().TimerString((maxTimerValue * normalizedValue));
                labelX.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            }
            for(int j = 0; j<=separatorCount; j++)
            {
                RectTransform labelY = Instantiate(labelTemplate);
                labelY.SetParent(graphContainer);
                labelY.gameObject.SetActive(true);
                float normalizedValue = j * 1f / separatorCount;
                labelY.anchoredPosition = new Vector2(-20f, normalizedValue * graphHeight);
                int value = (int)Mathf.Round(normalizedValue * yMaximum);
                labelY.GetComponent<TMPro.TextMeshProUGUI>().text = (value).ToString();
                labelY.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            }
        }
        public float MaximumY(float maxValue)
        {
            int length = maxValue.ToString().Length;
            float largestDigit = maxValue/Mathf.Pow(10, length-1);
            float rest = maxValue%Mathf.Pow(10, length-1);
            maxValue = (int)largestDigit * Mathf.Pow(10, length-1);
            
            largestDigit = rest/Mathf.Pow(10, length-2); 
            if(largestDigit >= 0) largestDigit = (int)largestDigit + 1;
            maxValue += largestDigit * Mathf.Pow(10, length-2);
            

            return maxValue;
        }
        private void CreateLines(Vector2 positionA, Vector2 positionB)
        {
            GameObject gameObject = new GameObject("Connection", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().color = color;

            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            Vector2 direction = (positionA - positionB).normalized;
            float distance = Vector2.Distance(positionA, positionB);

            rectTransform.anchorMin = new Vector2(0,0);
            rectTransform.anchorMax = new Vector2(0,0);
            rectTransform.sizeDelta = new Vector2(distance,3f);
            rectTransform.anchoredPosition = positionB + direction * distance * 0.5f;
            float angl = Vector3.Angle(positionB, positionA);
            
            angl = Mathf.Atan2(positionB.y - positionA.y, positionB.x - positionA.x);
            angl *= 180/Mathf.PI;
            rectTransform.localEulerAngles = new Vector3(0,0,angl);
        }
        public void RemoveGraph()
        {
            foreach(Transform child in graphContainer.transform)
            {
                if(child.gameObject.name != "Background") Destroy(child.gameObject);
            }
        }
        public void ShowUnitsGraph()
        {
            RemoveGraph();
            Dictionary<float, int> values = new Dictionary<float,int>();
            foreach(DataRecord record in dataSet.datas)
            {
                values.Add(record.time, record.units);
            }
            color = new Color(128,0,0);
            ShowGraph(values);
        }
        public void ShowMoneysGraph()
        {
            RemoveGraph();
            Dictionary<float, int> values = new Dictionary<float,int>();
            foreach(DataRecord record in dataSet.datas)
            {
                values.Add(record.time, record.money);
            }
            color = new Color(0,255,0);
            ShowGraph(values);
        }
        public void ShowPointsGraph()
        {
            RemoveGraph();
            Dictionary<float, int> values = new Dictionary<float,int>();
            foreach(DataRecord record in dataSet.datas)
            {
                values.Add(record.time, (int)Points(record.time, record.money, record. units));
            }
            color = new Color(128,128,0);
            ShowGraph(values);
        }
        public float Points(float time, int money, int units)
        {
            if(time==0) time = 1;
            float points = (money + units * unitsPointModifier)/time;
            return points;
        }
        public void DestroyDatas()
        {
            Destroy(GameObject.Find("PlayerData"));
            Destroy(GameObject.Find("EnemyData"));
        }
    }
    
}
