using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KZ.Cursor;

namespace Buildings
{
    public class PlayerBuilder : MonoBehaviour
    {
        public static PlayerBuilder instance;
        public bool isHoldingAScheme = false;
        [SerializeField] BuildingBasic buildingType;
        public Transform parentObject;

        [SerializeField] private Camera camera;
        private Transform scheme;
        private Color schemeColor;

        [SerializeField] private UI.PlayerActions actionList = null;

        private Position cursorPosition = new Position();
        private bool isFreeSpace = true;
        private void Awake()
        {
            instance = this;
            parentObject = GameObject.Find("PlayerBuildings").transform;
        }
        void Update()
        {
            if (scheme != null)
            {
                scheme.transform.position = cursorPosition.getMousePosition();
                isFreeSpace = CheckIfFreeSpace(
                    cursorPosition.getMousePosition(),
                    new Vector2(
                        scheme.GetComponent<SpriteRenderer>().bounds.size.x,
                        scheme.GetComponent<SpriteRenderer>().bounds.size.y
                        ));
                if (isFreeSpace)
                {
                    schemeColor.a = 0.75f;
                    scheme.GetComponent<SpriteRenderer>().color = schemeColor;
                }
                else scheme.GetComponent<SpriteRenderer>().color = new Color(128f, 0, 0, 0.75f);
            }
            if (isHoldingAScheme && Input.GetMouseButtonDown(0) && isFreeSpace)
            {
                SpawnNewBuilding(camera.ScreenToWorldPoint(Input.mousePosition), buildingType.name);
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
            GameObject.Find("PlayerStatistics").GetComponent<Statistics.Statistics>().money -= buildingType.baseStats.cost;
            GameObject building = Instantiate(
                buildingType.playerPrefab,
                new Vector3(mousePosition.x,mousePosition.y, mousePosition.y/1000),
                Quaternion.identity,
                parentObject.Find(buildingType.name.Replace(" ", "") + "s")
                );

            //TODO to function, the same in PlayerManager.cs and createBuilding
            Player.PlayerBuilding playerBuilding = building.GetComponent<Player.PlayerBuilding>();

            BuildingBasic settings = BuildingHandler.instance.GetBuildingStats(buildingType.name.ToLower());
            playerBuilding.baseStats = settings.baseStats;
            //disable functions of building and reduce health
            playerBuilding.isBuilded = false;
            playerBuilding.TurnOnOffFunctions(false);
            playerBuilding.gameObject.GetComponentInChildren<Core.HealthHandler>().
                SetHealthStats(playerBuilding.baseStats.health, playerBuilding.baseStats.armor, 1);
            AstarPath.active.Scan();
        }
        public void SpawnScheme(string objectName)
        {
            buildingType = IsBuilding(objectName);
            if (buildingType == null) return;

            isHoldingAScheme = true;
            scheme = Instantiate(
                buildingType.playerPrefab.transform.GetChild(1),
                cursorPosition.getMousePosition(),
                Quaternion.identity,
                parentObject.Find(buildingType.name.Replace(" ","") + "s")
                );
            schemeColor = scheme.GetComponent<SpriteRenderer>().color;
            schemeColor.a = 0.75f;
            scheme.GetComponent<SpriteRenderer>().color = schemeColor;
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
        public bool CheckIfFreeSpace(Vector2 center, Vector2 size)
        {
            var checkSpace = Physics2D.OverlapBox(center, size, 0);
            if (checkSpace == null) return true;
            return false;
        }
    }
}
