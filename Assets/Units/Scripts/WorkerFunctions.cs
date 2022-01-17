using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public class WorkerFunctions : MonoBehaviour
    {
        UnitRTS unit;
        [SerializeField] float buildingSpeedMultiply = 1;
        public bool isRepairing = false;
        public GameObject targetToRepair;

        private void Awake()
        {
            unit = gameObject.GetComponent<UnitRTS>();    
        }

        void Update()
        {
            if (unit.IfCommand == true)
                isRepairing = false;
            if (isRepairing == true)
                GoToRepairing();
        }

        public void SetRepairValues(bool Repair, GameObject target = null)
        {
            isRepairing = Repair;
            targetToRepair = target;
        }

        public void Repair()
        {
            var targetHealthHandler = targetToRepair
                .GetComponentInChildren<Core.HealthHandler>();
            var distanceToTarget = gameObject.GetComponent<UnitRTS>()
                .DistanceBetweenColliders(targetToRepair.transform);

            if (targetToRepair != null)
            {
                if (unit.atkCooldown <= 0 && distanceToTarget <= unit.baseStats.atkRange)
                {
                    RepairAnimation(true);
                    targetHealthHandler.GiveHealth(unit.baseStats.damage * buildingSpeedMultiply);
                    unit.atkCooldown = unit.baseStats.atkSpeed;
                }
            }
            else RepairAnimation(false);
        }

        public void GoToRepairing()
        {
            if(targetToRepair != null 
                && targetToRepair.GetComponentInChildren<Buildings.BuildingRTS>().isBuilded)
            {
                targetToRepair = null;
                RepairAnimation(false);
                isRepairing = false;
            }
            else if(targetToRepair != null)
            {
                unit.MoveToTarget(targetToRepair.transform);
                Repair();
            }
            else RepairAnimation(false);
        }
        
        public void RepairAnimation(bool TurnOn)
        {
            if (TurnOn)
            {
                float attackDirection = 0;
                unit.animator.SetBool("IfAttack", true);

                attackDirection = targetToRepair.transform.position.x - gameObject.transform.position.x;
                if (attackDirection > 0) attackDirection = 1;
                else attackDirection = -1;
                unit.animator.SetFloat("AttackDirection", attackDirection);
            }
            else unit.animator.SetBool("IfAttack", false);
        }
    }
}
