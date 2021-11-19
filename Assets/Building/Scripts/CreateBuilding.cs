using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class CreateBuilding : MonoBehaviour
    {
        public bool isHoldingAScheme = false;
        [SerializeField]
        BuildingBasic buildingType;
        public Transform parentObject;

        private void Awake()
        {
            parentObject = GameObject.Find("PlayerBuildings").transform;
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                isHoldingAScheme = true;
            }
            if (isHoldingAScheme && Input.GetMouseButtonDown(0))
            {
                SpawnNewBuilding();
            }
            if (isHoldingAScheme && Input.GetMouseButtonDown(1))
            {
                isHoldingAScheme = false;
            }
        }
        public void SpawnNewBuilding()
        {
            Instantiate(buildingType.buildingPrefab, parentObject.GetChild(0));
        }
    }
}
