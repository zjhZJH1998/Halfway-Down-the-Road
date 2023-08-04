using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum arrowType
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}
public class positionArrow : MonoBehaviour
{
    // Start is called before the first frame update
    

    void Start()
    {
      
        
    }
    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ReachEndPoint,selfDestroy);
    }
    private void selfDestroy()
    {
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ReachEndPoint, selfDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
