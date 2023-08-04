using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class isWin : MonoBehaviour
{

    public GameObject winPanel;
    public GameObject bossPanel;
    private void Awake()
    {
        winPanel = transform.Find("WinPanel").gameObject;
        bossPanel = transform.Find("BossLevelPanel").gameObject;
        EventCenter.AddListener(EventDefine.Win,OnWin);
        EventCenter.AddListener(EventDefine.BossLevel,OnBossLevel);
    }

    private void OnWin()
    {
        if (winPanel != null)
        {

            winPanel.SetActive(true);
        }

    }

    private void OnBossLevel()
    {
        if (winPanel != null)
        {
            bossPanel.SetActive(true);
            bossPanel.GetComponentInChildren<Button>().onClick.AddListener(OnFightButtonClicked);
        }

    }
    private void OnFightButtonClicked()
    {
        EventCenter.Broadcast(EventDefine.LoadBattleField);
    }
}
