using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CardManager : MonoBehaviour
{

    public GameObject[] prefabList;
    private Transform Wizard;

    [SerializeField]
    private Vector3 offset;

    public static CardManager Instance;

    private List<int> upgradeList;

    private void Awake()
    {
        Time.timeScale = 1;
        Wizard = GameObject.FindWithTag("Target").transform;
        EventCenter.AddListener<CardType>(EventDefine.SummonFinished, Summon);
        EventCenter.AddListener<Transform>(EventDefine.SummonCopy, SummonCopy);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<CardType>(EventDefine.SummonFinished, Summon);
        EventCenter.RemoveListener<Transform>(EventDefine.SummonCopy, SummonCopy);
    }

    private void Summon(CardType carType)
    {
        switch (carType)
        {
            case CardType.Swordsman:
                if(levelManager._instance.checkUpgrade(1) && !levelManager._instance.checkUpgrade(0))
                {
                    if (levelManager._instance.checkUpgrade(0))
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            GameObject go = Instantiate(prefabList[0], (Wizard.position + offset * Wizard.GetComponent<Wizard>().flip), Quaternion.identity);
                            go.GetComponent<Minion_SwordsMan>().InitMinion(0);
                            EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, -1);
                            EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, -1);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Instantiate(prefabList[0], (Wizard.position + offset * Wizard.GetComponent<Wizard>().flip * (i - 1)), Quaternion.identity);
                            EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, -1);
                            EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, -1);
                        }
                    }
                }
                else if (levelManager._instance.checkUpgrade(0) && !levelManager._instance.checkUpgrade(1)) 
                { 
                    {
                        GameObject go = Instantiate(prefabList[0], (Wizard.position + offset * Wizard.GetComponent<Wizard>().flip), Quaternion.identity);
                        go.GetComponent<Minion_SwordsMan>().InitMinion(0);
                        EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, -1);
                        EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, -1);
                    }
                }
                else
                {
                    GameObject go = Instantiate(prefabList[0], (Wizard.position + offset * Wizard.GetComponent<Wizard>().flip), Quaternion.identity);
                    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, -1);
                    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, -1);
                }
                break;
            case CardType.Archor:
                Instantiate(prefabList[1], (Wizard.position + offset * Wizard.GetComponent<Wizard>().flip), Quaternion.identity);
                EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, -1);
                EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, -2);
                break;
            case CardType.MetalPolymer:
                GameObject go_mp = Instantiate(prefabList[2], (Wizard.position + offset * Wizard.GetComponent<Wizard>().flip), Quaternion.identity);
                if (levelManager._instance.checkUpgrade(2)) go_mp.GetComponent<Minion_MP>().initMinion(2);
                EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, -3);
                EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, -2);
                break;
            case CardType.WaterElement:
                GameObject go_we = Instantiate(prefabList[3], (Wizard.position + offset * Wizard.GetComponent<Wizard>().flip), Quaternion.identity);
                if(levelManager._instance.checkUpgrade(3)) go_we.GetComponent<Minion_WE>().InitMinion(3);
                EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceBlue, SetResourceNumType.changeNum, -4);
                EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, -2);
                break;
            case CardType.SP:
                GameObject go_sp = Instantiate(prefabList[4], (Wizard.position + offset * Wizard.GetComponent<Wizard>().flip), Quaternion.identity);
                if (levelManager._instance.checkUpgrade(4)) go_sp.GetComponent<Minion_sp>().initMinion(4);
                EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, -2);
                EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceBlue, SetResourceNumType.changeNum, -5);
                break;
            case CardType.FG:
                GameObject go_fg =  Instantiate(prefabList[5], (Wizard.position + offset * Wizard.GetComponent<Wizard>().flip), Quaternion.identity);
                if (levelManager._instance.checkUpgrade(5)) go_fg.GetComponent<Minion_FG>().initMinion(5);
                EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, -2);
                EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, -5);
                break;
            case CardType.Skull:
                if(levelManager._instance.checkUpgrade(6))
                {
                    float flip = 1.5f;
                    for (int i = 0; i < 3; i++)
                    {
                        GameObject go_skull = Instantiate(prefabList[6], (Wizard.position + offset * flip), Quaternion.identity);
                        go_skull.GetComponent<Minion_Skull>().InitMinion();
                        flip -= 1.0f;
                    }
                    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, -1);
                    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, -2);
                }
                else
                {
                    int flip = 1;
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(prefabList[6], (Wizard.position + offset * flip), Quaternion.identity);
                        flip *= -1;
                    }
                    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceGreen, SetResourceNumType.changeNum, -1);
                    EventCenter.Broadcast(EventDefine.ResourceNum, ResourceType.ResourceRed, SetResourceNumType.changeNum, -2);
                }

                break;
        }
    }


    private void SummonCopy(Transform transform)
    {
        Vector3 offset = new Vector3(1.0f, 0,0);
        for(int i = 0;i<2;i++)
        {
            GameObject go = Instantiate(prefabList[5], transform.position + (i - 1) * offset, Quaternion.identity);
            go.GetComponent<Minion_FG>().InitSmallCopy();
        }

    }
}
