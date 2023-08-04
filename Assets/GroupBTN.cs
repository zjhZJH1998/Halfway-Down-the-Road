using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupBTN : MonoBehaviour
{
    public int BNTID = 0;
    public upgradeSelection type;
    public AudioSource upgrade;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        levelManager._instance.upgradesList[(int)type] = true;
        levelManager._instance.upgradesLeftList.Remove((int)type);
        upgrade.Play();
        EventCenter.Broadcast(EventDefine.CloseUpgradePanel);
        Debug.Log(type.ToString());
    }
}
