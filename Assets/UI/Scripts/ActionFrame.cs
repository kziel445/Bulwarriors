using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActionFrame : MonoBehaviour
    {
        public static ActionFrame instance;
        [SerializeField] private Button actionButton;
        [SerializeField] private Transform actionListUI;

        public List<Button> buttons = new List<Button>();
        private PlayerActions actionList = null;

        public List<float> spawningQueueTimer = new List<float>();
        public List<GameObject> spawnQueue = new List<GameObject>();

        public Transform spawnPoint = null;

        private void Awake()
        {
            instance = this;
        }
        public void SetActionButtons(PlayerActions actions, Transform spawnLocation)
        {
            spawnPoint = spawnLocation;
            actionList = actions;
            if(actions.basicUnits.Count>0)
            {
                foreach(Units.UnitBasic unit in actions.basicUnits)
                {
                    Button button = Instantiate(actionButton, actionListUI);
                    button.name = unit.name;
                    button.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = unit.icon;
                    buttons.Add(button);
                }
            }
            if(actions.basicBuildings.Count>0)
            {
                foreach(Buildings.BuildingBasic building in actions.basicBuildings)
                {
                    Button button = Instantiate(actionButton, actionListUI);
                    button.name = building.name;
                    buttons.Add(button);
                }
            }
        }
        public void ClearActions()
        {
            Debug.Log("Clear buttons");
            foreach(Button button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
        public void StartQueueTimer(string objectToSpawn)
        {
            if (IsUnit(objectToSpawn))
            {
                Units.UnitBasic unit = IsUnit(objectToSpawn);
                spawningQueueTimer.Add(unit.spawnTime);
                spawnQueue.Add(unit.playerPrefab);

            }
            else if (IsBuilding(objectToSpawn))
            {
                Buildings.BuildingBasic building = IsBuilding(objectToSpawn);
                spawningQueueTimer.Add(building.spawnTime);
                spawnQueue.Add(building.buildingPrefab);
            }
            else Debug.Log($"{objectToSpawn} is not spawnable");

            if(spawnQueue.Count == 1)
            {
                ActionTimer.instance.StartCoroutine(ActionTimer.instance.SpawnQueue());
            }
            else if(spawnQueue.Count == 0)
            {
                ActionTimer.instance.StopAllCoroutines();
            }

        }
        public void Spawn()
        {
            Instantiate(
                spawnQueue[0], 
                new Vector3(spawnPoint.position.x - 0.5f, spawnPoint.position.y, spawnPoint.position.z - 0.5f), 
                Quaternion.identity
                );
            
        }
        private Units.UnitBasic IsUnit(string name)
        {
            if(actionList.basicUnits.Count>0)
            {
                foreach(Units.UnitBasic unit in actionList.basicUnits)
                {
                    if(unit.name == name)
                    {
                        return unit;
                    }
                }
            }
            return null;
        }
        private Buildings.BuildingBasic IsBuilding(string name)
        {
            if (actionList.basicUnits.Count > 0)
            {
                foreach (Buildings.BuildingBasic building in actionList.basicBuildings)
                {
                    if (building.name == name)
                    {
                        return building;
                    }
                }
            }
            return null;
        }

    }

}