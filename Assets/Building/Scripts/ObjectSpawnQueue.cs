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

        public List<float> spawningQueueTimer = new List<float>();
        public List<GameObject> spawnQueue = new List<GameObject>();
        public List<Units.UnitBasic.unitType> spawnTypes = new List<Units.UnitBasic.unitType>();

        public Transform objectToStoreUnits;
        private UI.PlayerActions actionList = null;
        private Statistics.Statistics statistics;
        private Statistics.Data data;
        private bool isPlayer;
       
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            try
            {
                if(gameObject.name.Contains("Player"))
                {
                    objectToStoreUnits = GameObject.Find("PlayerUnits").transform;
                    actionList = gameObject.GetComponent<Player.PlayerBuilding>().baseStats.actions;
                    statistics = GameObject.Find("PlayerStatistics").GetComponent<Statistics.Statistics>();
                    data = GameObject.Find("PlayerData").GetComponent<Statistics.Data>();
                    isPlayer = true;
                }
                else 
                {
                    objectToStoreUnits = GameObject.Find("EnemyUnits").transform;
                    actionList = gameObject.GetComponent<Buildings.Enemy.EnemyBuilding>().baseStats.actions;
                    statistics = GameObject.Find("EnemyStatistics").GetComponent<Statistics.Statistics>();
                    data = GameObject.Find("EnemyData").GetComponent<Statistics.Data>();
                    isPlayer = false;
                }
            }
            catch
            {
                Debug.LogWarning("Units goes wrong object " + actionList + " " + objectToStoreUnits);
            }
        }
        public void StartQueueTimer(string objectToSpawn)
        {
            if (IsUnit(objectToSpawn))
            {
                Units.UnitBasic unit = IsUnit(objectToSpawn);
                statistics.money -= unit.baseStats.cost;
                spawningQueueTimer.Add(unit.spawnTime);

                if(isPlayer) spawnQueue.Add(unit.playerPrefab);
                else spawnQueue.Add(unit.enemyPrefab);
                
                spawnTypes.Add(unit.type);
                gameObject.transform.GetComponentInChildren<Text>().text = spawningQueueTimer.Count.ToString();
            }
            else Debug.Log($"{objectToSpawn} is not spawnable");
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
            GameObject unit = Instantiate(
                spawnQueue[0],
                new Vector3(
                    transform.position.x,
                    transform.position.y - gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().bounds.size.y/2,
                    transform.position.z
                    ),
                Quaternion.identity,
                objectToStoreUnits.Find(objectName.Replace(" ", ""))
                );
            objectName = objectName.Substring(0, objectName.Length - 1).ToLower();
            //TODO to function, the same in PlayerManager.cs and createBuilding
            Units.UnitRTS sideUnit = unit.GetComponent<Units.UnitRTS>();

            Units.UnitBasic settings = Units.UnitHandler.instance.GetUnitSettings(objectName);
            sideUnit.baseStats = settings.baseStats;
            sideUnit.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = settings.classColor;
            sideUnit.GetComponent<Pathfinding.AIPath>().maxSpeed = settings.baseStats.movementSpeed;
            data.unitsRecruted++;
        }
        public Units.UnitBasic IsUnit(string name)
        {
            if (actionList.basicUnits.Count > 0)
            {
                foreach (Units.UnitBasic unit in actionList.basicUnits)
                {
                    Debug.Log(unit.name + " " + name);
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


