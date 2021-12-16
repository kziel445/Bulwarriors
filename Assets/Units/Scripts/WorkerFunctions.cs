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
                Repair();
        }
        public void SetRepairValues(bool Repair, GameObject target = null)
        {
            isRepairing = Repair;
            targetToRepair = target;
        }
        public void Repair()
        {
            var targetHealthHandler = targetToRepair.GetComponentInChildren<Core.HealthHandler>();
            var distanceToTarget = Vector2.Distance(gameObject.transform.position, transform.position);
            if (unit.atkCooldown <= 0 && distanceToTarget <= unit.baseStats.atkRange)
            {
                RepairAnimation(true, targetToRepair.transform);
                unit.animator.SetBool("IfAttack", true);
                //Debug.Log("Hit!");
                targetHealthHandler.GiveHealth(unit.baseStats.damage * buildingSpeedMultiply);
                unit.atkCooldown = unit.baseStats.atkSpeed;
                if (targetHealthHandler.baseHealth == targetHealthHandler.currentHealth)
                {
                    RepairAnimation(false);
                    SetRepairValues(false);
                    return;
                }
            }
            else if(distanceToTarget > unit.baseStats.atkRange) unit.MoveTo(targetToRepair.transform.position);
            else if(distanceToTarget <= unit.baseStats.atkRange) RepairAnimation(false);
        }
        public void RepairAnimation(bool TurnOn, Transform targetPosition = null)
        {
            if (TurnOn)
            {
                float attackDirection = 0;
                unit.animator.SetBool("IfAttack", true);

                attackDirection = targetPosition.position.x - gameObject.transform.position.x;
                if (attackDirection > 0) attackDirection = 1;
                else attackDirection = -1;
                unit.animator.SetFloat("AttackDirection", attackDirection);
            }
            else unit.animator.SetBool("IfAttack", false);
        }
    }
}
