using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class ObjectSpawnQueue : MonoBehaviour
    {
        public static ObjectSpawnQueue instance;

        public List<Button> buttons = new List<Button>();
        private UI.PlayerActions actionList = null;

        public List<float> spawningQueueTimer = new List<float>();
        public List<GameObject> spawnQueue = new List<GameObject>();
        public List<Units.UnitBasic.unitType> spawnTypes = new List<Units.UnitBasic.unitType>();

        public Transform objectToStoreUnits;
       


        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            actionList = gameObject.GetComponent<Player.PlayerBuilding>().baseStats.actions;
            try
            {
                objectToStoreUnits = GameObject.Find("PlayerUnits").transform;
            }
            catch
            {
                Debug.Log("Units goes wrong object");
            }
            
        }
        public void StartQueueTimer(string objectToSpawn)
        {
            
            if (IsUnit(objectToSpawn))
            {
                
                Units.UnitBasic unit = IsUnit(objectToSpawn);
                GameObject.Find("PlayerStatistics").GetComponent<Statistics.Statistics>().money -= unit.baseStats.cost;
                spawningQueueTimer.Add(unit.spawnTime);
                spawnQueue.Add(unit.playerPrefab);
                spawnTypes.Add(unit.type);
                gameObject.transform.GetComponentInChildren<Text>().text = spawningQueueTimer.Count.ToString();

            }
            else Debug.Log($"{objectToSpawn} is not spawnable");
            Debug.Log("Corutine");
            if (spawnQueue.Count == 1)
            {
                gameObject.GetComponent<SpawnTimer>().StartCoroutine(gameObject.GetComponent<SpawnTimer>().SpawnQueue());
            }
            else if (spawnQueue.Count == 0)
            {
                gameObject.GetComponent<SpawnTimer>().StopAllCoroutines();
            }

        }
        public void Spawn()
        {
            
            string objectName = spawnTypes[0].ToString() + "s";
            //objectName = spawnQueue[0].GetComponent<Units.Player.PlayerRTS>().baseStats.unitClass;
            GameObject unit = Instantiate(
                spawnQueue[0],
                //spawnTMP[0].Item4,
                new Vector3(
                    gameObject.transform.position.x,
                    gameObject.transform.position.y - 0.5f,
                    gameObject.transform.position.z
                    ),
                Quaternion.identity,
                objectToStoreUnits.Find(objectName.Replace(" ", ""))
                );
            objectName = objectName.Substring(0, objectName.Length - 1).ToLower();
            //TODO to function, the same in PlayerManager.cs and createBuilding
            Units.Player.PlayerRTS playerUnit = unit.GetComponent<Units.Player.PlayerRTS>();

            Units.UnitBasic settings = Units.UnitHandler.instance.GetUnitSettings(objectName);
            playerUnit.baseStats = settings.baseStats;
            playerUnit.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = settings.classColor;
            GameObject.Find("PlayerData").GetComponent<Statistics.Data>().unitsRecruted ++;
        }
        private Units.UnitBasic IsUnit(string name)
        {
            if (actionList.basicUnits.Count > 0)
            {
                foreach (Units.UnitBasic unit in actionList.basicUnits)
                {
                    if (unit.name == name)
                    {
                        return unit;
                    }
                }
            }
            return null;
        }
        
    }
}


