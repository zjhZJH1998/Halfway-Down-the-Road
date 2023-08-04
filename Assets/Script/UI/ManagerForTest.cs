using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManagerForTest : MonoBehaviour
{

    //TODO: 当player行进到一半退出游戏后，再次打开游戏时，waypoint会重新刷新，并且不会弹出taskPanel
    
    public static ManagerForTest _instance;
    private Transform cur;
    [SerializeField]
    public Transform TargetTown;
    [SerializeField]
    public GameObject CurrentTown;
    [SerializeField]
    private GameObject taskPanel;
    [SerializeField]
    private GameObject townPanel;
    [SerializeField]
    private waypoint[] waypoints;
    [SerializeField]
    private bool isInitialize; //when reach another town, set this value to false
    [SerializeField]
    private bool isInMiddleWay; // when reload UI Scene from other scenes(battle scene etc.), set this value to true

    public GameObject upgradeText;
    public GameObject timerPanel;

    private int levelID;

    //public static ManagerForTest Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = FindObjectOfType(typeof(ManagerForTest)) as ManagerForTest;
    //        }
    //        return _instance;
    //    }
    //}
    private void Awake()
    {

        Time.timeScale = 1;
        cur = GameObject.FindWithTag("Cur").transform;
        EventCenter.AddListener(EventDefine.HatStartMove, onHatStartMove);
        EventCenter.AddListener<Transform>(EventDefine.TaskPanel, showTaskPanel);
        EventCenter.AddListener(EventDefine.TownPanel, showTownPanel);
        EventCenter.AddListener(EventDefine.ReachEndPoint, ReachEndPoint);
        //PlayerPrefs.SetString("currentTown", "TownA"); //set current town to test
        CurrentTown = GameObject.Find(PlayerPrefs.GetString("currentTown"));
        TargetTown = GameObject.Find(PlayerPrefs.GetString("targetTown")).transform;
        taskPanel = GameObject.Find("Panels").transform.Find("Task Goal").gameObject;
        townPanel = GameObject.Find("Panels").transform.Find("Spell Atelier").gameObject;
        //TargetTown = GameObject.Find(PlayerPrefs.GetString("targetTown")).transform;
    }
    private void Start()
    {
        isInitialize = false;
        if(PlayerPrefs.GetInt("isInMiddleWay") == 1) // this scene is reload from other scenes
        {
            isInMiddleWay = true;
            //showTaskPanel(TargetTown);
            EventCenter.Broadcast(EventDefine.HatContinueMove);
        }
        else
        {
            isInMiddleWay = false;
        }
        CurrentTown.GetComponent<Town>().isHere = true;
        //cur.position = CurrentTown.transform.position;
         
    }


    private void OnDestroy()
    {
        

        EventCenter.RemoveListener<Transform>(EventDefine.TaskPanel, showTaskPanel);
        EventCenter.RemoveListener(EventDefine.TownPanel, showTownPanel);
        EventCenter.RemoveListener(EventDefine.ReachEndPoint, ReachEndPoint);
       
    }
    private void ReachEndPoint()
    {

        cur.position = TargetTown.position;
        TargetTown.GetComponent<Town>().isHere = true;
        CurrentTown.GetComponent <Town>().isHere = false;
        PlayerPrefs.SetString("currentTown", TargetTown.name);
        CurrentTown = TargetTown.gameObject;
        isInitialize = false;
        PlayerPrefs.SetInt("isInMiddleWay", 0);
        EventCenter.Broadcast(EventDefine.CheckTaskIsFinish,TargetTown);
        EventCenter.Broadcast(EventDefine.ClockDown);
        if (PlayerPrefs.GetInt("ThreatLevel") >= 2)
        {
            EventCenter.Broadcast(EventDefine.UpgradePanelOpen);
        }
        if(PlayerPrefs.GetInt("Instruction")==1|| PlayerPrefs.GetInt("Instruction") == 2)
        {
            timerPanel.SetActive(true);
        }
       

    }
    private void showTaskPanel(Transform targetTown)
    {
        //搜索用   新建playerprefs 1：进入当前level是否大于等于2  新建playerprefs 2：判断战斗结束后是否胜利 结合两者判断是否打开升级奖励
        levelID = levelManager._instance.GetLevelID(CurrentTown.GetComponent<Town>().townID, targetTown.GetComponent<Town>().townID);
       
        int skullNum = (levelManager._instance.GetThreatValue(levelID) + 10) / 10;
        PlayerPrefs.SetInt("ThreatLevel",skullNum);

        taskPanel.SetActive(true);
        if (skullNum > 1)
        {
            upgradeText.SetActive(true);
        }
        else
        {
            upgradeText.SetActive(false);
        }
        taskPanel.GetComponent<TaskPanel>().SetSKullNum(skullNum);        


        TargetTown = targetTown;
        PlayerPrefs.SetString("targetTown", TargetTown.name);
        if (PlayerPrefs.GetInt("isInMiddleWay") == 1) // this scene is reload from other scenes
        {
            isInMiddleWay = true;
            //showTaskPanel(TargetTown);
        }
        else
        {
            isInMiddleWay = false;
        }
        if (isInMiddleWay)
        {
            reloadWaypoints();

            //isInMiddleWay = false;
            //PlayerPrefs.SetInt("isInMiddleWay", 0); //not reload now

        }else if (!isInitialize)
        {
            InitializeWaypoints();
            EventCenter.Broadcast(EventDefine.StartFromNewTown);
        } 
    }
    private void showTownPanel()
    {
        townPanel.SetActive(true);
    }
    private void InitializeWaypoints()
    {
        waypoints = FindObjectsOfType<waypoint>();


        int count = 2;
        bool hasBattlePoint = false;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i].name == "startPoint")
            {
                waypoints[i].w_Type = waypointType.Start;
                count--;
            }
            else if (waypoints[i].name == "endPoint")
            {
                waypoints[i].w_Type = waypointType.End;
                count--;
            }
            else
            {
                if (i != waypoints.Length - count - 1)
                {
                    int type = UnityEngine.Random.Range(0, 2);
                    Debug.Log("switch type"+type);
                    switch (type)
                    {
                        case 0:
                            waypoints[i].w_Type = waypointType.Normal;
                            break;
                        case 1:
                            waypoints[i].w_Type = waypointType.Battle;
                            hasBattlePoint = true;
                            break;
                    }
                }
                else
                {
                    if (!hasBattlePoint)
                    {
                        waypoints[i].w_Type = waypointType.Battle;
                        hasBattlePoint = true;
                    }
                    else
                    {
                        int type = UnityEngine.Random.Range(0, 2);
                        Debug.Log("switch type" + type);
                        switch (type)
                        {
                            case 0:
                                waypoints[i].w_Type = waypointType.Normal;
                                break;
                            case 1:
                                waypoints[i].w_Type = waypointType.Battle;
                                hasBattlePoint = true;
                                break;
                        }
                    }
                }


            }

        }
        isInitialize = true;
        
    }
    private void reloadWaypoints()
    {
        waypoints = FindObjectsOfType<waypoint>();
        foreach (waypoint wp in waypoints)
        {
            switch (wp.name)
            {
                case "startPoint":
                    wp.w_Type = waypointType.Start;
                    break;
                case "endPoint":
                    wp.w_Type = waypointType.End;
                    break;
                case "waypoint1":
                    wp.w_Type = (waypointType)PlayerPrefs.GetInt("waypoint1Type");
                    break;
                case "waypoint2":
                    wp.w_Type = (waypointType)PlayerPrefs.GetInt("waypoint2Type");
                    break;
                case "waypoint3":
                    wp.w_Type = (waypointType)PlayerPrefs.GetInt("waypoint3Type");
                    break;
            }
        }
    }
    
    private void onHatStartMove()
    {
        if(CurrentTown != null && TargetTown != null)
        {
            this.levelID = levelManager._instance.GetLevelID(CurrentTown.GetComponent<Town>().townID, TargetTown.GetComponent<Town>().townID);
            levelManager._instance.OnThreatUp(this.levelID, 10);
            int levelID = levelManager._instance.GetLevelID(CurrentTown.GetComponent<Town>().townID, TargetTown.gameObject.GetComponent<Town>().townID);
            GameManager._instance.levelID = levelID;
        }
    }

}
