using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public bool cheatsAvailable = false;
    public bool getKeys = false;
    public string pressed = "";
    public string keyCode;
    TMPro.TMP_InputField text;
    Dictionary<string, string> commandBase = new Dictionary<string,string>()
    {
        {"help", "few commands to \"help\""},
        {"win", " you win"},
        {"lose", "you lose"},
        {"money", "+500 gold{not working}"}

    };
    private void Start() 
    {
        text = panel.transform.Find("Text").GetComponent<TMPro.TMP_InputField>();
    }
    void Update()
    {   
        if(!cheatsAvailable)
        {
            foreach(KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
                {}
                else if(Input.GetKeyDown(vKey))
                {    
                    if(vKey.ToString()==keyCode) keyCode = null;
                    else keyCode = vKey.ToString();
                }
            }
            if(keyCode == "C")
            {
                pressed = keyCode;
                keyCode = null;
                getKeys = true;
            }
            if(getKeys && keyCode!=null)
            {
                pressed += keyCode;
                keyCode = null;
            }

            if(pressed == "CHEAT") cheatsAvailable = true;
            else if(pressed.Length>6) getKeys =false;
        }
        else 
        {
            if(Input.GetKeyDown(KeyCode.BackQuote) && cheatsAvailable)
            {
                if(panel.active == true) panel.SetActive(false);
                else if(panel.active == false) panel.SetActive(true);
            }
            if(Input.GetKeyDown(KeyCode.Return) && cheatsAvailable)
            {
                string[] lines = text.text.Split('\n');
                string command = lines[lines.Length-1].ToLower();
                text.text += "\n";
                if(command=="win") WinCode(true);
                else if(command=="lose")WinCode(false);
                else if(command=="help")HelpCode();
                else if(command=="money")HelpCode();
                else text.text += "Invalid command\n";
            }
        }
    }
    private void WinCode(bool ifWin)
    {
        Transform parentBuidling;
        if(ifWin) parentBuidling = GameObject.Find("EnemyBuildings").transform;
        else parentBuidling = GameObject.Find("PlayerBuildings").transform;
        foreach(Transform group in parentBuidling)
        {
            foreach(Transform structure in group)
            {
                Destroy(structure.gameObject);
            }
        }
    }
    private void HelpCode()
    {
        string addText = "";
        foreach(var element in commandBase)
        {
            addText+=element.Key + " - " + element.Value +"\n";
        }
        text.text+=addText;
    }

}
