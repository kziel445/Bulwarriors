using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public class BuildingRTS : MonoBehaviour, IClickable
    {
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
    }

}
