using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public void win()
    {
        if (PlayerPrefs.GetInt("Instruction") == 1)
        {
            PlayerPrefs.SetInt("Instruction", 2);
            PlayerPrefs.SetInt("battleInstruction", 1);
        }
        EventCenter.Broadcast(EventDefine.LoadMap);
    }
}
