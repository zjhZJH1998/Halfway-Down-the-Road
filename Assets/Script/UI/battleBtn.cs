using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public void startBattle()
    {
        //if(PlayerPrefs.GetInt("Instruction")==1)
        EventCenter.Broadcast(EventDefine.LoadBattleField);
    }
}
