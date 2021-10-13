using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit: MonoBehaviour, IClickable
{
    void Update()
    {
     	//TODO this whole script should be added to unit main unit script
    }

	public void Click()
	{
        
		Debug.Log("Unit");
	}

}
