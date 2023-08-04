using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatInPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public waypoint[] waypoints;
    [SerializeField]
    private int waypointIndex;
    private int waypointsLength;
    private bool isHatMoving;
    [SerializeField]
    private float speed;
    [SerializeField]
    private AudioSource movementAudio;
    private void Awake()
    {
        EventCenter.AddListener(EventDefine.HatInPanelMove, HatStartMove);
        EventCenter.AddListener(EventDefine.HatInPanelMoveFinished, HatFinishMove);
        EventCenter.AddListener(EventDefine.StartFromNewTown, startFromNewTown);
        EventCenter.AddListener(EventDefine.ReachBattlePoint,reachBattlePoint);
        waypoints = this.transform.parent.GetComponentsInChildren<waypoint>();
        movementAudio = GetComponent<AudioSource>();
    }
    void Start()
    {
        
        isHatMoving = false;
        speed = 200f;
        //PlayerPrefs.SetInt("waypointIndex", 0);
        waypointIndex = PlayerPrefs.GetInt("waypointIndex");
        waypointsLength = waypoints.Length;
        this.transform.position = waypoints[waypointIndex].transform.position+ new Vector3(100,0,0);
        
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.HatInPanelMove, HatStartMove);
        EventCenter.RemoveListener(EventDefine.HatInPanelMoveFinished, HatFinishMove);
        EventCenter.RemoveListener(EventDefine.StartFromNewTown, startFromNewTown);
        EventCenter.RemoveListener(EventDefine.ReachBattlePoint, reachBattlePoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHatMoving)
        {
            
            
            this.transform.position = Vector2.MoveTowards(this.transform.position, waypoints[waypointIndex].transform.position+new Vector3(100,0,0), speed * Time.deltaTime);
            if(this.transform.position.y <= waypoints[waypointIndex].transform.position.y)
            {
                
                isHatMoving = false;
                EventCenter.Broadcast(EventDefine.HatInPanelMoveFinished);
            }
        }
    }
    private void startFromNewTown()
    {
        //Debug.Log(waypoints[waypointIndex].name);
        this.transform.position = waypoints[waypointIndex].transform.position + new Vector3(100, 0, 0);
        
    }
    private void HatStartMove()
    {
       
        
        waypointIndex = PlayerPrefs.GetInt("waypointIndex")+1;
        if(waypointIndex< waypointsLength)
        {
            isHatMoving = true;
            movementAudio.Play();
        }        
    }
    private void HatFinishMove()
    {
        if(waypoints[waypointIndex].w_Type == waypointType.End)
        {
            waypointIndex = 0;
            EventCenter.Broadcast(EventDefine.ReachEndPoint);
            
        }else if(waypoints[waypointIndex].w_Type == waypointType.Battle)
        {
            EventCenter.Broadcast(EventDefine.ReachBattlePoint);
        }
        PlayerPrefs.SetInt("waypointIndex", waypointIndex);
    }
    private void reachBattlePoint()
    {

        PlayerPrefs.SetInt("waypoint1Type", (int)waypoints[1].w_Type);
        PlayerPrefs.SetInt("waypoint2Type", (int)waypoints[2].w_Type);
        PlayerPrefs.SetInt("waypoint3Type", (int)waypoints[3].w_Type);
        EventCenter.Broadcast(EventDefine.LoadBattleField);

    }
    
}
