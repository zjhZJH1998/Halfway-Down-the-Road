using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishMapInstri : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thisPanel;
    public GameObject finishPanel;
    public void finishMapInstr()
    {
        thisPanel.SetActive(false);
        PlayerPrefs.SetInt("mapInstruction", 1);
        if (PlayerPrefs.GetInt("battleInstruction") == 1)
        {
            finishPanel.SetActive(true);
        }
    }
}
