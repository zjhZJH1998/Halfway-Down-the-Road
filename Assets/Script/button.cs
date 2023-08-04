using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void setValue()
    {
        Debug.Log("set resource1,and resource2 to 77");
        EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen,SetResourceNumType.setNum,88);
        EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.setNum,88);
        EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceBlue, SetResourceNumType.setNum, 88);
        EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceBlack, SetResourceNumType.setNum, 88);
        EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceWhite, SetResourceNumType.setNum, 88);

    }
}
