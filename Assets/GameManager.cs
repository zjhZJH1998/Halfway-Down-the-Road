using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    
    // current road wizzard on
    public int levelID;

    public List<bool> UpgradesList;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return _instance;
        }
    }

    private void Awake()
    {
        levelID = -1;
       // ResetValue();
        EventCenter.AddListener(EventDefine.LoadBattleField, LoadBattleField);
        EventCenter.AddListener(EventDefine.LoadMap, LoadMap);
        EventCenter.AddListener(EventDefine.ReStartScene, ReStartScene);
    }

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.LoadBattleField, LoadBattleField);
        EventCenter.RemoveListener(EventDefine.LoadMap, LoadMap);
        EventCenter.RemoveListener(EventDefine.ReStartScene, ReStartScene);
    }

    private void LoadBattleField()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void LoadMap()
    {
        PlayerPrefs.SetInt("isReload", 1);
        SceneManager.LoadScene("TestForUI");
        
    }

    private void ReStartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

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
