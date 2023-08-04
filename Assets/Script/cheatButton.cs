using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheatButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void ResetValue()
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

    }
}
