using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBtn : MonoBehaviour
{
    private Button btn;
    public EventDefine ed_upgrade;

    private void Awake()
    {
       // btn = GetComponent<Button>();

       // btn.onClick.AddListener(click2Upgrade);
    }


    private void click2Upgrade()
    {
        EventCenter.Broadcast(ed_upgrade);
    }
}
