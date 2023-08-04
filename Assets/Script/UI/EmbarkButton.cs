using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbarkButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void Embark()
    {
        if (PlayerPrefs.GetInt("isInMiddleWay") == 0) { EventCenter.Broadcast(EventDefine.HatStartMove); }
        else
        {
            EventCenter.Broadcast(EventDefine.HatContinueMove);
        }

    }
}
