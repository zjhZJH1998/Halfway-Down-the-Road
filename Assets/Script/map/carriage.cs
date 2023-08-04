using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carriage : MonoBehaviour
{
    // Start is called before the first frame update
    public Waypoint waypointToGo;
    private float speed = 2.0f;
    public bool goBattle;
    public bool goCastle;
    public bool goCastle2;
    public string start;
    
    void Start()
    {
        goBattle = false;
        goCastle = false;
        goCastle2 = false;
        start = "castle";
    }
    private void Move(bool leftOrRight, string destination)
    {
        
        transform.position = Vector2.MoveTowards(transform.position,waypointToGo.transform.position,speed*Time.deltaTime);  
        if(transform.position == waypointToGo.transform.position)
        {
            
            if(waypointToGo.name == destination)
            {
                goBattle = false;
                goCastle = false;
                goCastle2 = false;
                start = destination;
            }
            else
            {
                if (leftOrRight)
                {
                    waypointToGo = waypointToGo.right;
                }
                else
                {
                    waypointToGo = waypointToGo.left;
                }
              
            }
            
        }
    }
    private void decidePath()
    {
        
        if (goBattle)
        {
            if (start == "castle")
            {
                Move(true, "Battle");
            }else if(start == "castle2")
            {
                Move(false, "Battle");
            }
        }else if (goCastle)
        {
            Move(false, "castle");
        }else if (goCastle2)
        {
            Move(true, "castle2");
        }
    }

    // Update is called once per frame
    void Update()
    {
        decidePath();
    }
}
