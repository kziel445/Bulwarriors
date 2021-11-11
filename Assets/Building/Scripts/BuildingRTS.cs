using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class BuildingRTS : MonoBehaviour, IClickable
    {
        //statistics
        public BuildingStatTypes.Base baseStats;
        public Image healthBarAmount;
        public float currentHealth;

        [System.Serializable]
        public class BuildUnits
        {
            public Units.UnitBasic[] basicUnits;

        }
        public void Click()
        {
            Debug.Log("Building options in UI");
            Debug.Log("Building health");
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public virtual void HandleHealth()
        {
            healthBarAmount.fillAmount = currentHealth / baseStats.health;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        public void TakeDamage(float damage)
        {
            //TODO: do better formula for fight
            damage -= baseStats.armor;
            if (damage <= 0) damage = 1;
            //Debug.Log(damage);
            currentHealth -= damage;
        }
        public void Die()
        {
            Destroy(gameObject);
        }
    }

}
