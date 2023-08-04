using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public void gameStart()
    {
        destroyDontDestroy();
        resetResource();
        resetGame();
        PlayerPrefs.SetInt("Instruction", 0);
        PlayerPrefs.SetInt("battleInstruction", 0);
        PlayerPrefs.SetInt("mapInstruction", 0);
        SceneManager.LoadScene("TestForUI");

    }
    private void resetResource()
    {
        //EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.setNum, 88);
        //EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.setNum, 88);
        //EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceBlue, SetResourceNumType.setNum, 88);
        //EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceBlack, SetResourceNumType.setNum, 88);
        //EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceWhite, SetResourceNumType.setNum, 88);
        PlayerPrefs.SetInt("ResourceGreenNum", 40);
        PlayerPrefs.SetInt("ResourceRedNum", 40);
        PlayerPrefs.SetInt("ResourceBlueNum", 40);
        
    }
    private void resetGame()
    {
        PlayerPrefs.SetString("TaskToA", "Send these corns to Town A, I`ll give you 10 gem Green and 10 gem Red");
        PlayerPrefs.SetString("TaskToB", "Send these corns to Town B, I`ll give you 10 gem Green and 10 gem Red");
        PlayerPrefs.SetString("TaskToC", "Send these corns to Town C, I`ll give you 10 gem Green and 10 gem Red");
        PlayerPrefs.SetString("TaskToD", "Send these corns to Town D, I`ll give you 10 gem Green and 10 gem Red");
        PlayerPrefs.SetString("TaskToE", "Send these corns to Town E, I`ll give you 10 gem Green and 10 gem Red");
        PlayerPrefs.SetString("TaskToF", "Send these corns to Town F, I`ll give you 10 gem Green and 10 gem Red");
        PlayerPrefs.SetInt("isDoingTask", 0); // 0 means not doing one task, 1 means is doing one task
        PlayerPrefs.SetString("currentTown", "TownA");
        PlayerPrefs.SetString("targetTown", "TownB");
        PlayerPrefs.SetFloat("currentTime", 0f);
        PlayerPrefs.SetFloat("totalTime", 99999f);
        PlayerPrefs.SetInt("eventPoint0", 0);
        PlayerPrefs.SetInt("eventPoint1", 0);
        PlayerPrefs.SetInt("eventPoint2", 0);
        PlayerPrefs.SetInt("eventPoint3", 0);
        PlayerPrefs.SetFloat("timeOfEventPoint0", 99999f);
        PlayerPrefs.SetFloat("timeOfEventPoint1", 99999f);
        PlayerPrefs.SetFloat("timeOfEventPoint2", 99999f);
        PlayerPrefs.SetFloat("timeOfEventPoint3", 99999f);
        PlayerPrefs.SetInt("isInMiddleWay", 0);
        PlayerPrefs.SetInt("ThreatLevel", 0);

    }
    private void destroyDontDestroy()
    {
        if (FindObjectOfType<ResourceManager>() == null) {
            Debug.Log("new game");
            return;
        }
        Debug.Log("old game");
        GameObject _resourceManager = FindObjectOfType<ResourceManager>().gameObject;
        GameObject _levelManager = FindObjectOfType<levelManager>().gameObject;
        GameObject _gameManager = FindObjectOfType<GameManager>().gameObject;
        GameObject _myEventSystem = FindObjectOfType<MyEventSys>().gameObject;
        Destroy(_resourceManager);
        Destroy(_levelManager);
        Destroy(_gameManager);
        Destroy(_myEventSystem);

    }
}
