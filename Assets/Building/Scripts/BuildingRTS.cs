using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRTS : MonoBehaviour, IClickable
{
    public void Click()
    {
        Debug.Log("Building");
    }

    public int Layer() { return gameObject.layer; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
