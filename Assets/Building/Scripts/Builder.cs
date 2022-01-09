using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KZ.Cursor;

namespace Buildings
{
    public class Builder : MonoBehaviour
    {
        public static Builder instance;
        public bool isHoldingAScheme = false;
        [SerializeField] BuildingBasic buildingType;
        public Transform parentObject;

        private Transform scheme;
        private Color schemeColor;

        [SerializeField] private UI.PlayerActions actionList = null;

        private Position cursorPosition = new Position();

        private void Awake()
        {
            instance = this;
            if(gameObject.name.Contains("Player"))
            {
                parentObject = GameObject.Find("PlayerBuildings").transform;
            }
            else if(gameObject.name.Contains("Enemy"))
            {
                parentObject = GameObject.Find("EnemyBuildings").transform;
            }
        }
        
        public void SpawnNewBuilding(Vector2 mousePosition, string buildingToSpawn)
        {
            GameObject buildingPrefab = null;
            Debug.Log("cmon" + buildingToSpawn);
            buildingType = IsBuilding(buildingToSpawn);
            if (buildingType == null) return;
            Debug.Log("Halo?");
            if(gameObject.name.Contains("Player"))
            {
                GameObject.Find("PlayerStatistics").GetComponent<Statistics.Statistics>().money -= buildingType.baseStats.cost;
                buildingPrefab = buildingType.playerPrefab;
            }
            else if(gameObject.name.Contains("Enemy"))
            {
                Debug.Log(GameObject.Find("EnemyStatistics").GetComponent<Statistics.Statistics>());
                GameObject.Find("EnemyStatistics").GetComponent<Statistics.Statistics>().money -= buildingType.baseStats.cost;
                buildingPrefab = buildingType.enemyPrefab;
            }
            else 
            {
                Debug.LogWarning("Statistics not found");
            }

            GameObject building = Instantiate(
                buildingPrefab,
                new Vector3(mousePosition.x,mousePosition.y, mousePosition.y/1000),
                Quaternion.identity,
                parentObject.Find(buildingType.name.Replace(" ", "") + "s")
                );
            //TODO to function, the same in PlayerManager.cs and createBuilding
            BuildingRTS playerBuilding = building.GetComponent<BuildingRTS>();

            BuildingBasic settings = BuildingHandler.instance.GetBuildingStats(buildingType.name.ToLower());
            playerBuilding.baseStats = settings.baseStats;
            //disable functions of building and reduce health
            playerBuilding.isBuilded = false;
            playerBuilding.TurnOnOffFunctions(false);
            playerBuilding.gameObject.GetComponentInChildren<Core.HealthHandler>().
                SetHealthStats(playerBuilding.baseStats.health, playerBuilding.baseStats.armor, 1);
        }
        public BuildingBasic IsBuilding(string name)
        {
            if (actionList.basicBuildings.Count > 0)
            {
                foreach (BuildingBasic building in actionList.basicBuildings)
                {
                    if (building.name == name)
                    {
                        return building;
                    }
                }
            }
            return null;
        }
        public bool CheckIfFreeSpace(Vector2 center, Vector2 size)
        {
            var checkSpace = Physics2D.OverlapBox(center, size, 0);
            if (checkSpace == null) return true;
            return false;
        }
        public void GoBuild()
        {
            
        }
    }
}
