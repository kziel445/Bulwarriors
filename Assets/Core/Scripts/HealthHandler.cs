using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Units;
using Buildings;

namespace Core
{
    public class HealthHandler : MonoBehaviour
    {
        public Image healthBarAmount;
        public float currentHealth;
        public float baseHealth;
        public float baseArmor;
        // Start is called before the first frame update
        void Start()
        {
            if (gameObject.GetComponentInParent<Units.Player.PlayerRTS>() !=null)
            {
                var component = gameObject.GetComponentInParent<Units.Player.PlayerRTS>();
                baseHealth = component.baseStats.health;
                baseArmor = component.baseStats.armor;

            }
            else if(gameObject.GetComponentInParent<Units.Enemy.EnemyRTS>() != null)
            {
                var component = gameObject.GetComponentInParent<Units.Enemy.EnemyRTS>();
                baseHealth = component.baseStats.health;
                baseArmor = component.baseStats.armor;
            }
            else if(gameObject.GetComponentInParent<Buildings.Player.PlayerBuilding>() != null)
            {
                var component = gameObject.GetComponentInParent<Buildings.Player.PlayerBuilding>();
                baseHealth = component.baseStats.health;
                baseArmor = component.baseStats.armor;
            }
            else if(gameObject.GetComponentInParent<Buildings.Enemy.EnemyBuilding>() != null)
            {
                var component = gameObject.GetComponentInParent<Buildings.Enemy.EnemyBuilding>();
                baseHealth = component.baseStats.health;
                baseArmor = component.baseStats.armor;
            }
                currentHealth = baseHealth;
        }

        // Update is called once per frame
        void Update()
        {
            HandleHealth();
        }
        public void HandleHealth()
        {
            healthBarAmount.fillAmount = currentHealth / baseHealth;

            if (currentHealth <= 0)
            {
                if (InputManager.InputHandler.instance.selectedUnitRTSList.Contains(gameObject.GetComponentInParent<Core.Interactables.Interactable>()))
                {
                    InputManager.InputHandler.instance.selectedUnitRTSList.Remove(gameObject.GetComponentInParent<Core.Interactables.Interactable>());
                }
                Die();
            }
        }
        public void TakeDamage(float damage)
        {
            //TODO: do better formula for fight
            damage -= baseArmor;
            if (damage <= 0) damage = 1;
            //Debug.Log(damage);
            currentHealth -= damage;
        }
        public void Die()
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

}
