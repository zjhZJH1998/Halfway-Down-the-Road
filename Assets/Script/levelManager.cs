using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum upgradeSelection
{
    swords_1 = 0,
    swords_2 = 1,
    mp_1 = 2,
    we_1 = 3,
    sp_1 = 4,
    fg_1 = 5,
    sk_1 = 6,
}

public class levelManager : MonoBehaviour
{
    public static levelManager _instance;
    
    private Dictionary<Tuple<int, int>, int> levels;
    private Dictionary<int, int> levelThreat;
    public List<bool> upgradesList;
    public List<int> upgradesLeftList;
    public int WeekLeft;

    private const int upgradeCount = 7;
    public int upgradeleft;

    private void Awake()
    {

        initLevelID();
        initLevelThreat();
        WeekLeft = 5;
        upgradeleft = upgradeCount;

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);


        EventCenter.AddListener<int, int >(EventDefine.ThreatUp, OnThreatUp);
        EventCenter.AddListener(EventDefine.ClockDown, OnClockDown);
    }

    private void Start()
    {
        upgradesList = new List<bool>(new bool[upgradeCount]);
        upgradesLeftList = new List<int>(new int[upgradeCount]);
        for(int i = 0;i< upgradesLeftList.Count; i++)
        {
            upgradesLeftList[i] = i;
        }
    }

    public void OnThreatUp(int levelID,int value)
    {
        
        if (levelThreat.ContainsKey(levelID))
        {
            int tmp = levelThreat[levelID] + value;
            if (tmp > 30) tmp = 30;
            levelThreat[levelID] = tmp;
        }
    } 

    private void OnClockDown()
    {
        WeekLeft--;
        if (WeekLeft <= 0)
        {
            GameManager._instance.levelID = 5;
            EventCenter.Broadcast(EventDefine.BossLevel);
        }
    }


    public int GetThreatValue(int levelID)
    {
        return levelThreat[levelID];
    }

    public int GetLevelID(int id1,int id2)
    {
        return id1 < id2 ? levels[new Tuple<int, int>(id1, id2)] : levels[new Tuple<int, int>(id2, id1)];
    }    

    private void initLevelID()
    {
        levels = new Dictionary<Tuple<int, int>, int>();
        levels.Add(new Tuple<int, int>(0, 1), 0);
        levels.Add(new Tuple<int, int>(0, 2), 1);
        levels.Add(new Tuple<int, int>(1, 2), 2);
    }

    private void initLevelThreat()
    {
        levelThreat = new Dictionary<int, int>();
        levelThreat.Add(0, 0);
        levelThreat.Add(1, 0);
        levelThreat.Add(2, 0);
        levelThreat.Add(5, 5);
    }

    public bool checkUpgrade(int id)
    {
        return upgradesList[id];
    }
}
