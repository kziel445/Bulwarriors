using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cursor;

namespace Buildings
{
    public class CreateBuilding : MonoBehaviour
    {
        public bool isHoldingAScheme = false;
        [SerializeField]
        BuildingBasic buildingType;
        public Transform parentObject;

        [SerializeField] private Camera camera;
        private Transform scheme;

        private Position cursorPosition = new Position();

        private void Awake()
        {
            parentObject = GameObject.Find("PlayerBuildings").transform;
        }
        // Update is called once per frame
        void Update()
        {
            if (scheme != null) scheme.transform.position = cursorPosition.getMousePosition();
            if (Input.GetKeyDown(KeyCode.B))
            {
                isHoldingAScheme = true;
                scheme = Instantiate(
                    buildingType.buildingPrefab.transform.GetChild(1),
                    cursorPosition.getMousePosition(),
                    Quaternion.identity,
                    parentObject
                    );

            }
            if (isHoldingAScheme && Input.GetMouseButtonDown(0))
            {
                SpawnNewBuilding(camera.ScreenToWorldPoint(Input.mousePosition));
            }
            if (isHoldingAScheme && Input.GetMouseButtonDown(1))
            {
                isHoldingAScheme = false;
                Destroy(scheme.gameObject);
            }
        }
        public void SpawnNewBuilding(Vector2 mousePosition)
        {
            
            Instantiate(buildingType.buildingPrefab, mousePosition, Quaternion.identity, parentObject.GetChild(0));
        }
        public void SpawnScheme()
        {
            
        }
    }
}
