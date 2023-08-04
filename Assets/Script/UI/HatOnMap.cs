using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HatOnMap : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Vector3 speed;

    public bool isMoving;

    private ManagerForTest MapManager;

    [SerializeField]
    private float currentTime;

    [SerializeField]
    private float totalTime;

    [SerializeField]
    private GameObject positionArrow;
    [SerializeField]
    private GameObject battlePanel;
    [SerializeField]
    private GameObject eventPanel;
    [SerializeField]
    private Text eventPanelText;

    [SerializeField]
    private int[] eventPoints=new int[4];
    [SerializeField]
    private float[] timeOfEventPoints = new float[4];
    [SerializeField]
    private int currentPoint;
    [SerializeField]
    private AudioSource footstep;
    private void Awake()
    {
        EventCenter.AddListener(EventDefine.HatStartMove, HatStartMove);
        EventCenter.AddListener(EventDefine.HatContinueMove, HatContinueMove);
        MapManager = FindObjectOfType<ManagerForTest>();
        battlePanel = GameObject.Find("Panels").transform.Find("BattlePanel").gameObject;
        eventPanel = GameObject.Find("Panels").transform.Find("EventPanel").gameObject;
        eventPanelText = GameObject.Find("Panels").transform.Find("EventPanel").Find("Canvas").Find("EventText").gameObject.GetComponent<Text>();
        currentTime = PlayerPrefs.GetFloat("currentTime");
        totalTime = PlayerPrefs.GetFloat("totalTime");
        for(int i = 0; i < eventPoints.Length; i++)
        {
            eventPoints[i] = PlayerPrefs.GetInt("eventPoint"+i.ToString());
            timeOfEventPoints[i] = PlayerPrefs.GetFloat("timeOfEventPoint" + i.ToString());
        }
        currentPoint = -1;
        for (int i = 0; i < timeOfEventPoints.Length; i++)
        {
            if (currentTime >= timeOfEventPoints[i]) currentPoint += 1;
        }
        if (currentTime >= totalTime) currentPoint += 1;
        isMoving = false;


    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.HatStartMove, HatStartMove);
        EventCenter.RemoveListener(EventDefine.HatContinueMove, HatContinueMove);
    }
    void Start()
    {
        
        
        if (currentPoint == -1 || currentPoint >3)
        {
            this.transform.position = MapManager.CurrentTown.transform.position;
        }
        else
        {
            this.transform.position = MapManager.CurrentTown.transform.position +
                currentTime * 4*((MapManager.TargetTown.position - MapManager.CurrentTown.transform.position).normalized);
        }
        
    }

    void HatStartMove()
    {

        speed = 4*((MapManager.TargetTown.position - MapManager.CurrentTown.transform.position).normalized);
        totalTime = (MapManager.TargetTown.position - MapManager.CurrentTown.transform.position).x / (speed.x );
        PlayerPrefs.SetFloat("totalTime", totalTime);
        generateEventPoints();
        PlayerPrefs.SetFloat("currentTime", 0f);
        currentTime = 0f;
        currentPoint = -1;
        PlayerPrefs.SetInt("isInMiddleWay", 1);
        generateArrows();
        isMoving = true;
        playAudio();


    }
    void generateEventPoints()
    {

        float t=0f;
        for (int i = 0;i < eventPoints.Length; i++)
        {
            int eventPointType = MyEventSys._instance.returnEvent();   //0 normal point,1 battle point,2 event point
            
            eventPoints[i] = eventPointType;
            PlayerPrefs.SetInt("eventPoint"+i.ToString(), eventPointType);
            float eventPointTime = Random.Range(t+ (totalTime * (i) / 4), t+(totalTime*(i+1)/4));
            timeOfEventPoints[i] = eventPointTime;
            PlayerPrefs.SetFloat("timeOfEventPoint"+i.ToString(),eventPointTime);

        }
    }
    void HatContinueMove()
    {

       // Debug.Log("revceive");
        speed = 4*((MapManager.TargetTown.position - MapManager.CurrentTown.transform.position).normalized);
        PlayerPrefs.SetInt("isInMiddleWay", 1);
        generateArrows();
        isMoving = true;
        playAudio();
    }
    void check()
    {
        if (!isMoving) return;
        if (currentTime >= timeOfEventPoints[0] && currentPoint < 0)
        {
            currentPoint = 0;
            startEvent(eventPoints[0]);
            return;
        }
        if (currentTime >= timeOfEventPoints[1] && currentPoint < 1)
        {
            currentPoint = 1;
            startEvent(eventPoints[1]);
            return;
        }
        if (currentTime >= timeOfEventPoints[2] && currentPoint < 2)
        {
            currentPoint = 2;
            startEvent(eventPoints[2]);
            return;
        }
        if (currentTime >= timeOfEventPoints[3] && currentPoint < 3)
        {
            currentPoint = 3;
            startEvent(eventPoints[3]);
            return;
        }
        if (currentTime >= totalTime)
        {
            isMoving = false;
            stopAudio();
            EventCenter.Broadcast(EventDefine.ReachEndPoint);
            return;
        }
    }
    
    void startEvent(int eventType)
    {
        PlayerPrefs.SetFloat("currentTime", currentTime);
        stopAudio();
        if (eventType == 1)
        {
            Time.timeScale = 0;
            battlePanel.SetActive(true);
            //EventCenter.Broadcast(EventDefine.LoadBattleField);
        }else if(eventType == 2)
        {
            Time.timeScale = 0;
            eventPanelText.text = "Do you want to trade 10 blue resouce for 10 red resource?";
            eventPanel.SetActive(true);
            
            EventCenter.Broadcast(EventDefine.EventPanel, eventType);
        }
        else if (eventType == 3)
        {
            Time.timeScale = 0;
            eventPanelText.text = "Do you want to trade 10 red resouce for trade 10 green resource?";
            eventPanel.SetActive(true);

            EventCenter.Broadcast(EventDefine.EventPanel, eventType);
        }
        else if (eventType == 4)
        {
            Time.timeScale = 0;
            eventPanelText.text = "Do you want to trade 10 green resouce for 10 blue resource?";
            eventPanel.SetActive(true);

            EventCenter.Broadcast(EventDefine.EventPanel, eventType);
        }
    }
    void generateArrows()
    {
        float angle = Mathf.Rad2Deg *
            Mathf.Atan2(MapManager.TargetTown.position.y - MapManager.CurrentTown.transform.position.y, MapManager.TargetTown.position.x - MapManager.CurrentTown.transform.position.x);
        Vector2 startPoint = MapManager.CurrentTown.transform.position;
        Vector2 endPoint = MapManager.TargetTown.position;
        Vector2 distance = endPoint - startPoint;
        float dis =distance.magnitude;
        int num = (int)dis / 1; //一个箭头所占格子为1
        Vector2 offset = distance.normalized;
        for(int i = 1; i < num; i++)
        {
            Instantiate(positionArrow, startPoint + offset * i, Quaternion.Euler(0, 0, angle));
        }


    }
    void playAudio()
    {
        footstep.Play();
    }
    void stopAudio()
    {
        footstep.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            this.transform.position += speed*Time.deltaTime;
            currentTime += Time.deltaTime;
            check();
        }
    }
}
