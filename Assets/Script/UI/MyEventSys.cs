using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyEventSys : MonoBehaviour
{
    public static MyEventSys _instance;

    public List<int> eventList = new List<int>();
    public int index=0;
    private const int eventMaxNumber = 20;


    public static MyEventSys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(MyEventSys)) as MyEventSys;
            }
            return _instance;
        }
    }
    // Start is called before the first frame update
    void Start()
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

        initialEventList();
    }
    void initialEventList()
    {
        int battle = 7;
        int normal = 8;
        int exchange = 5;
        for (int i = 0; i < eventMaxNumber; i++){
            int temp = Random.Range(0, 5);
            while (!checkEvent(temp, ref normal, ref battle, ref  exchange))
            {
                temp = Random.Range(0, 5);
            }
            //Debug.Log("temp:"+temp+"normal"+normal+"battle:"+battle+"exchange:"+exchange);
            eventList.Add(temp);
        }
    }
    bool checkEvent(int temp, ref int normal, ref int battle, ref int exchange) {
        bool flag = true;
        switch (temp) { 
            case 0:
                if (normal - 1 < 0)
                {
                    flag = false;
                }
                else
                {
                    normal -= 1;
                }
                
                break;
            case 1:
                if (battle - 1 < 0)
                {
                    flag = false;
                }
                else
                {
                    battle -= 1;
                }
                break;
            case 2:
            case 3:
            case 4:
                if (exchange - 1 < 0)
                {
                    flag = false;
                }
                else
                {
                    exchange -= 1;
                }
                break;
        }
        return flag;
    }
    public int returnEvent()
    {
        if (index + 1 > eventMaxNumber) return -1;
        index += 1;
        return eventList[index-1];
    }

    // Update is called once per frame
    
}
