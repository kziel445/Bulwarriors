using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Core
{
    public class InheritFromBase : MonoBehaviour
    {
        public static InheritFromBase instance;
        public IEnumerable<Interactables.Interactable> GetAll()
        {

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(Interactables.Interactable)))
                .Select(type => Activator.CreateInstance(type) as Interactables.Interactable);
        }
        private void Awake()
        {
            instance = this;
        }
        private void Update()
        {
            if(Input.GetKey(KeyCode.K))
            {
                Debug.Log("K is clicked");
                var zmienna = gameObject.GetComponent<Core.InheritFromBase>().GetAll().ToList();
                foreach(var x in zmienna)
                {
                    Debug.Log(x.GetType());
                }
            }
            
        }
    }
}

