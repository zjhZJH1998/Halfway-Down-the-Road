using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum waypointType
{
    Normal,   //normal point without any event
    Battle,   //point with battle event
    End,      //End Point
    Start,    //Start Point
    Boss,
}
public class waypoint : MonoBehaviour
{
    // Start is called before the first frame update
    public waypointType w_Type;
}
