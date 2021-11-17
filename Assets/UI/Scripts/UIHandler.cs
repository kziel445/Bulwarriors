using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InputManager;

namespace UI
{
    public class UIHandler : MonoBehaviour
    {
        public static UIHandler instance;
        public List<GameObject> unitsSelected;
        public GameObject frame;
        public Transform actionUI;

        public int playerUnits = 0;
        private float minPosX = -580, maxPosY = 37;

        private void Start()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {

            
        }
        public void ShowStatistics()
        {

        }
        public void UpdateSelectedUnits()
        {
            if (InputHandler.instance.selectedUnitRTSList.Count == 0)
            {
                foreach (var unit in unitsSelected)
                {
                    Destroy(unit);
                }
            }
            if (InputHandler.instance.selectedUnitRTSList.Count > 0)
            {
                float posX = minPosX;
                float posY = maxPosY;
                foreach (var unit in unitsSelected)
                {
                    Destroy(unit);
                }
                unitsSelected = new List<GameObject>();
                foreach (var unit in InputHandler.instance.selectedUnitRTSList)
                {
                    var sprite = unit.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    frame.transform.GetChild(1).GetComponent<Image>().sprite = sprite.sprite;
                    var image = (GameObject)Instantiate(frame, actionUI);
                    image.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);
                    unitsSelected.Add(image);
                    posX += 80;
                    if (posX > -minPosX)
                    {
                        posY -= 40;
                        posX = minPosX;
                    }
                }

            }
        }
    }
}

