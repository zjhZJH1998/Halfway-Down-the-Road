using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TaskType
{
    TaskToA,
    TaskToB,
    TaskToC,
    TaskToD,
    TaskToE,
    TaskToF,
}
public enum TownName
{
    TownA,
    TownB,
    TownC,
    TownD,
    TownE,
    TownF,
}
public class TaskManager : MonoBehaviour
{
    
    [SerializeField]
    private Transform TargetTown;
    [SerializeField]
    private GameObject CurrentTown;
    [SerializeField]
    private Text taskPanelText;

    public TaskType currentTask;


    [SerializeField]
    private AudioSource TaskFinishAudio;
    

    private void Awake()
    {
        CurrentTown = GameObject.Find(PlayerPrefs.GetString("currentTown"));

        taskPanelText = GameObject.Find("Panels").transform.Find("Task Goal").Find("Canvas").Find("TextFrame").Find("TaskText").gameObject.GetComponent<Text>();
        
        EventCenter.AddListener<Transform>(EventDefine.CheckTaskIsFinish,CheckTaskisFinish);
        EventCenter.AddListener(EventDefine.FinishTask, InitializeTask);
        TaskFinishAudio = GetComponent<AudioSource>();
       
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<Transform>(EventDefine.CheckTaskIsFinish, CheckTaskisFinish);
        EventCenter.RemoveListener(EventDefine.FinishTask, InitializeTask);
    }
    void Start()
    {
        //intialize some tasks for test
        //PlayerPrefs.SetString("TaskToA", "Send these corns to Town A, I`ll give you 10 gem Green and 10 gem Red");
        //PlayerPrefs.SetString("TaskToB", "Send these corns to Town B, I`ll give you 10 gem Green and 10 gem Red");
        //PlayerPrefs.SetString("TaskToC", "Send these corns to Town C, I`ll give you 10 gem Green and 10 gem Red");
        //PlayerPrefs.SetString("TaskToD", "Send these corns to Town D, I`ll give you 10 gem Green and 10 gem Red");
        //PlayerPrefs.SetString("TaskToE", "Send these corns to Town E, I`ll give you 10 gem Green and 10 gem Red");
        //PlayerPrefs.SetString("TaskToF", "Send these corns to Town F, I`ll give you 10 gem Green and 10 gem Red");
        //PlayerPrefs.SetInt("isDoingTask", 0); // 0 means not doing one task, 1 means is doing one task
        //PlayerPrefs.SetInt("currentTask", 0);
        currentTask = (TaskType)PlayerPrefs.GetInt("currentTask");
        //InitializeTask();
        //taskPanelText.text = PlayerPrefs.GetString(currentTask.ToString());
        


    }
    private void CheckTaskisFinish(Transform TargetTown)
    {
        //Debug.Log(((TownName)PlayerPrefs.GetInt("currentTask")).ToString());
        //Debug.Log(TargetTown.name);
        CurrentTown = GameObject.Find(PlayerPrefs.GetString("currentTown"));
        //if (((TownName)PlayerPrefs.GetInt("currentTask")).ToString() == TargetTown.name)
        //{
        //    PlayerPrefs.SetInt("isDoingTask", 0);
        //    //just use easiest way to implement task finished,
        //    //maybe TODO:finish task finish system?
        //    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, 10);
        //    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, 10);
        //    EventCenter.Broadcast(EventDefine.FinishTask);
        //    TaskFinishAudio.Play();
        //}
        switch (TargetTown.name)
        {
            case "TownA":
                EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, 25);
                break;
            case "TownB":
                EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, 25);
                break;
            case "TownC":
                EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceBlue, SetResourceNumType.changeNum, 25);
                break;
        }
        TaskFinishAudio.Play();
    }
    private void InitializeTask()
    {
        if (PlayerPrefs.GetInt("isDoingTask") == 0)
        {
            int a;
            switch (CurrentTown.name)
            {
                case "TownA":
                    a = Random.Range(0, 5);
                    currentTask = (TaskType)(a + 1);
                    PlayerPrefs.SetInt("currentTask", a + 1);
                    taskPanelText.text = PlayerPrefs.GetString(((TaskType)(a+1)).ToString());
                    break;
                case "TownB":
                    a = Random.Range(0, 5);
                    if (a == 1)
                    {
                        currentTask = (TaskType)(5);
                        PlayerPrefs.SetInt("currentTask", 5);
                        taskPanelText.text = PlayerPrefs.GetString(((TaskType)(5)).ToString());
                    }
                    else
                    {
                        currentTask = (TaskType)(a);
                        PlayerPrefs.SetInt("currentTask", a);
                        taskPanelText.text = PlayerPrefs.GetString(((TaskType)(a)).ToString());
                    }
                    break;
                case "TownC":
                    a = Random.Range(0, 5);
                    if (a == 2)
                    {
                        currentTask = (TaskType)(5);
                        PlayerPrefs.SetInt("currentTask", 5);
                        taskPanelText.text = PlayerPrefs.GetString(((TaskType)(5)).ToString());
                    }
                    else
                    {
                        currentTask = (TaskType)(a);
                        PlayerPrefs.SetInt("currentTask", a);
                        taskPanelText.text = PlayerPrefs.GetString(((TaskType)(a)).ToString());
                    }
                    break;
                case "TownD":
                    a = Random.Range(0, 5);
                    if (a == 3)
                    {
                        currentTask = (TaskType)(5);
                        PlayerPrefs.SetInt("currentTask", 5);
                        taskPanelText.text = PlayerPrefs.GetString(((TaskType)(5)).ToString());
                    }
                    else
                    {
                        currentTask = (TaskType)(a);
                        PlayerPrefs.SetInt("currentTask", a);
                        taskPanelText.text = PlayerPrefs.GetString(((TaskType)(a)).ToString());
                    }
                    break;
                case "TownE":
                    a = Random.Range(0, 5);
                    if (a == 4)
                    {
                        currentTask = (TaskType)(5);
                        PlayerPrefs.SetInt("currentTask", 5);
                        taskPanelText.text = PlayerPrefs.GetString(((TaskType)(5)).ToString());
                    }
                    else
                    {
                        currentTask = (TaskType)(a);
                        PlayerPrefs.SetInt("currentTask", a);
                        taskPanelText.text = PlayerPrefs.GetString(((TaskType)(a)).ToString());
                    }
                    break;
                case "TownF":
                    a = Random.Range(0, 5);
                    currentTask = (TaskType)(a);
                    PlayerPrefs.SetInt("currentTask", a);
                    taskPanelText.text = PlayerPrefs.GetString(((TaskType)(a)).ToString());
                    break;

            }
            PlayerPrefs.SetInt("isDoingTask", 1);
            Debug.Log("this task is" + PlayerPrefs.GetInt("currentTask"));
            
            //taskPanelText.GetComponent<Text>().text = PlayerPrefs.GetString(((TaskType)(PlayerPrefs.GetInt("currentTask"))).ToString());
        }
        
    }
    // Update is called once per frame
    
}
