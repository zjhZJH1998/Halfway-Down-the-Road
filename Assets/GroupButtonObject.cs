using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupButtonObject : MonoBehaviour
{
    public int buttonID;
    public GameObject panel;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(onclicked);
    }

    private void onclicked()
    {
        EventCenter.Broadcast(EventDefine.onShowDecButtonClicked, buttonID);
        panel.SetActive(true);
    }
}
