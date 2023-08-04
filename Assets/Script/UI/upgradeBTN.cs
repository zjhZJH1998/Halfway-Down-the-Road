using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeBTN : MonoBehaviour
{

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        EventCenter.Broadcast(EventDefine.UpgradePanelOpen);
    }
}
