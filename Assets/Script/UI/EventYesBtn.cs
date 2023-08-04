using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventYesBtn : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    private GameObject eventPanel;
    private int eventType;
    private ResourceManager resourceManager;
    public AudioSource footstep;
    private void Awake()
    {
        EventCenter.AddListener<int>(EventDefine.EventPanel, setEventType);
        resourceManager = FindObjectOfType(typeof(ResourceManager)) as ResourceManager;
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.EventPanel, setEventType);
    }
    private void setEventType(int eventType)
    {
        this.eventType = eventType;
    }
    public void eventYes()
    {
        switch (eventType)
        {
            case 2:
                if (resourceManager.checkResource(ResourceType.ResourceBlue, -10) && resourceManager.checkResource(ResourceType.ResourceRed, 10))
                {
                    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceBlue, SetResourceNumType.changeNum, -10);
                    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, 10);
                }
                break;
            case 3:
                if (resourceManager.checkResource(ResourceType.ResourceBlue, -10) && resourceManager.checkResource(ResourceType.ResourceRed, 10))
                {
                    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, -10);
                    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, 10);
                }
                break;
            case 4:
                if (resourceManager.checkResource(ResourceType.ResourceBlue, -10) && resourceManager.checkResource(ResourceType.ResourceRed, 10))
                {
                    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, -10);
                    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceBlue, SetResourceNumType.changeNum, 10);
                }
                break;
        }
        eventPanel.SetActive(false);
        footstep.Play();
        Time.timeScale = 1;
    }
}
