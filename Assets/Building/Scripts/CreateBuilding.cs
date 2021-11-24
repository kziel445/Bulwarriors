using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cursor;

namespace Buildings
{
    public class CreateBuilding : MonoBehaviour
    {
        public static CreateBuilding instance;
        public bool isHoldingAScheme = false;
        [SerializeField]
        BuildingBasic buildingType;
        public Transform parentObject;

        [SerializeField] private Camera camera;
        private Transform scheme;

        [SerializeField]
        private UI.PlayerActions actionList = null;

        private Position cursorPosition = new Position();

        private void Awake()
        {
            instance = this;
            parentObject = GameObject.Find("PlayerBuildings").transform;
            
        }
        private void Start()
        {
            
        }
        // Update is called once per frame
        void Update()
        {
            if (scheme != null) scheme.transform.position = cursorPosition.getMousePosition();

            if (isHoldingAScheme && Input.GetMouseButtonDown(0))
            {
                SpawnNewBuilding(camera.ScreenToWorldPoint(Input.mousePosition),buildingType.name);
                isHoldingAScheme = false;
                Destroy(scheme.gameObject);
            }
            if (isHoldingAScheme && Input.GetMouseButtonDown(1))
            {
                isHoldingAScheme = false;
                Destroy(scheme.gameObject);
            }
        }
        public void SpawnNewBuilding(Vector2 mousePosition, string buildingToSpawn)
        {

            GameObject.Find("PlayerStatistics").GetComponent<PlayerStats.Statistics>().money -= buildingType.baseStats.cost;
            GameObject building = Instantiate(
                buildingType.buildingPrefab,
                new Vector3(mousePosition.x,mousePosition.y, mousePosition.y/1000),
                Quaternion.identity,
                parentObject.Find(buildingType.name + "s")
                );

            //TODO to function, the same in PlayerManager.cs and createBuilding
            Buildings.Player.PlayerBuilding playerBuilding = building.GetComponent<Player.PlayerBuilding>();


            BuildingBasic settings = BuildingHandler.instance.GetBuildingStats(buildingType.name.ToLower());
            playerBuilding.baseStats = settings.baseStats;


        }
        public void SpawnScheme(string objectName)
        {
            buildingType = IsBuilding(objectName);
            if (buildingType == null) return;

            isHoldingAScheme = true;
            scheme = Instantiate(
                buildingType.buildingPrefab.transform.GetChild(1),
                cursorPosition.getMousePosition(),
                Quaternion.identity,
                parentObject.Find(buildingType.name + "s")
                );
        }
        private BuildingBasic IsBuilding(string name)
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
        
    }
}
