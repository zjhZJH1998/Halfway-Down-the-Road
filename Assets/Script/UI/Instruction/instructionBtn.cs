using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructionBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject nextPanel;
    public GameObject thisPanel;
    public void nextInstructionPanel()
    {
        thisPanel.SetActive(false);
        if(nextPanel != null)
        {
            nextPanel.SetActive(true);
        }
    }
}
