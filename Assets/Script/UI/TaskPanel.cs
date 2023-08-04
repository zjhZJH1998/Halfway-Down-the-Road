using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPanel : MonoBehaviour
{

    public GameObject levelThreatUI;
    public GameObject skullIcon;

    public int skullNum;

    private GameObject[] skulls;

    // Update is called once per frame
    private bool canClose;

    private void Awake()
    {
        skullNum = -1;
        skulls = new GameObject[3];
        EventCenter.AddListener(EventDefine.ReachEndPoint, canCloseTaskPanel);
        EventCenter.AddListener(EventDefine.HatInPanelMove, cantCloseTaskPanel);
        EventCenter.AddListener(EventDefine.HatStartMove, closeTaskPanel);
        EventCenter.AddListener(EventDefine.HatContinueMove, closeTaskPanel);
        if (PlayerPrefs.GetInt("isInMiddleWay") == 1)
        {
            canClose = false;
        }
        else
        {
            canClose = true;
        }
        
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ReachEndPoint, canCloseTaskPanel);
        EventCenter.RemoveListener(EventDefine.HatInPanelMove, cantCloseTaskPanel);
        EventCenter.RemoveListener(EventDefine.HatStartMove, closeTaskPanel);
        EventCenter.RemoveListener(EventDefine.HatContinueMove, closeTaskPanel);
    }
   void Update()
    {
        if (canClose)
        {
            if (Input.GetMouseButtonDown(1))
            {
                for(int i = 0; i < skullNum; i++)
                {
                    GameObject.Destroy(skulls[i]);
                    skulls[i] = null;
                }
                skullNum = -1;
                this.gameObject.SetActive(false);

            }
        }

    }

    public void SetSKullNum(int num)
    {
        skullNum = num > 3 ? 3 : num;
       // Debug.Log(num);
        for(int i = 0;i<skullNum;i++)
        {
            GameObject go = Instantiate(skullIcon, levelThreatUI.transform);
            go.transform.localPosition = new Vector3(-183 + i * 60, -95, 0.0f);
            skulls[i] = go;
        }
    }

    private void canCloseTaskPanel()
    {
        canClose = true;
    }
    private void cantCloseTaskPanel()
    {
        canClose = false;
    }
    private void closeTaskPanel()
    {
        this.gameObject.SetActive(false);
    }
}
