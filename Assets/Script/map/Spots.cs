using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spots : MonoBehaviour
{
    Vector2 intial;
    Vector2 bigger;
    public carriage myCarriage;
    private void OnMouseEnter()
    {
       
        transform.localScale = bigger;


    }
    private void OnMouseExit()
    {
        transform.localScale = intial;
    }
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        string name = this.name;
        Debug.Log(name);
        if(name == "castle")
        {
            if (myCarriage.start != "castle")
            {
                myCarriage.goCastle = true;
            }
                

        }else if(name == "battle")
        {
            if (myCarriage.start != "battle")
            {
                myCarriage.goBattle = true;
            }
            

        }else if(name == "castle2")
        {
            if (myCarriage.start != "castle2")
            {
                myCarriage.goCastle2 = true;
            }
                
        }
    }
    void Start()
    {
        intial = transform.localScale;
        bigger = transform.localScale * 1.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
