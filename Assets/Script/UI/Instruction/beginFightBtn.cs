using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beginFightBtn : MonoBehaviour
{
    public GameObject thisPanel;
    public void beginFight()
    {
        thisPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
