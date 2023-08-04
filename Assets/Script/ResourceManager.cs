using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ResourceType
{
    ResourceGreen,
    ResourceRed,
    ResourceBlue,
    ResourceBlack,
    ResourceWhite,
}
public enum SetResourceNumType
{
    setNum,    //reset resource num to a certain num
    changeNum,     // add to reduce resource num
}
public class ResourceManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static ResourceManager _instance;
    public int playerResourceGreenNum { get; set; }
    public int playerResourceRedNum { get; set; }
    public int playerResourceBlueNum { get; set; }

    public int playerResourceBlackNum { get; set; }

    public int playerResourceWhiteNum { get; set; }


    // eventcenter add<int>, reduce<int>.

    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(ResourceManager)) as ResourceManager;
            }
            return _instance;
        }
    }

    private void Awake()
    {
        EventCenter.AddListener<ResourceType, SetResourceNumType,int>(EventDefine.ResourceNum, ChangeResourceNum);

        playerResourceGreenNum = PlayerPrefs.GetInt("ResourceGreenNum");
        playerResourceRedNum = PlayerPrefs.GetInt("ResourceRedNum");
        playerResourceBlueNum = PlayerPrefs.GetInt("ResourceBlueNum");
        playerResourceBlackNum = PlayerPrefs.GetInt("ResourceBlackNum");
        playerResourceWhiteNum = PlayerPrefs.GetInt("ResourceWhiteNum");
    }
    private void OnDestroy()
    {

        EventCenter.RemoveListener<ResourceType, SetResourceNumType, int>(EventDefine.ResourceNum, ChangeResourceNum);
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
    private void ChangeResourceNum(ResourceType r_type, SetResourceNumType sr_type,int changeNum)
    {
        
        if(sr_type == SetResourceNumType.setNum)
        {
            switch (r_type)
            {
                case ResourceType.ResourceGreen:
                    PlayerPrefs.SetInt("ResourceGreenNum", changeNum);
                    playerResourceGreenNum = PlayerPrefs.GetInt("ResourceGreenNum");
                    break;
                case ResourceType.ResourceRed:
                    PlayerPrefs.SetInt("ResourceRedNum", changeNum);
                    playerResourceRedNum = PlayerPrefs.GetInt("ResourceRedNum");
                    break;
                case ResourceType.ResourceBlue:
                    PlayerPrefs.SetInt("ResourceBlueNum", changeNum);
                    playerResourceBlueNum = PlayerPrefs.GetInt("ResourceBlueNum");
                    break;
                case ResourceType.ResourceBlack:
                    PlayerPrefs.SetInt("ResourceBlackNum", changeNum);
                    playerResourceBlackNum = PlayerPrefs.GetInt("ResourceBlackNum");
                    break;
                case ResourceType.ResourceWhite:
                    PlayerPrefs.SetInt("ResourceWhiteNum", changeNum);
                    playerResourceWhiteNum = PlayerPrefs.GetInt("ResourceWhiteNum");
                    break;
            }
        }else if (sr_type == SetResourceNumType.changeNum)  
        {
            //positive changeNum means add resource, negative changeNum means reduce resource
            switch (r_type)
            {
                case ResourceType.ResourceGreen:
                    if (!checkResource(r_type, changeNum)) return;
                    PlayerPrefs.SetInt("ResourceGreenNum", playerResourceGreenNum + changeNum);
                    playerResourceGreenNum = PlayerPrefs.GetInt("ResourceGreenNum");
                    break;
                case ResourceType.ResourceRed:
                    if (!checkResource(r_type, changeNum)) return;
                    PlayerPrefs.SetInt("ResourceRedNum", playerResourceRedNum + changeNum);
                    playerResourceRedNum = PlayerPrefs.GetInt("ResourceRedNum");
                    break;
                case ResourceType.ResourceBlue:
                    if (!checkResource(r_type, changeNum)) return;
                    PlayerPrefs.SetInt("ResourceBlueNum", playerResourceBlueNum + changeNum);
                    playerResourceBlueNum = PlayerPrefs.GetInt("ResourceBlueNum");
                    break;
                case ResourceType.ResourceBlack:
                    if (!checkResource(r_type, changeNum)) return;
                    PlayerPrefs.SetInt("ResourceBlackNum", playerResourceBlackNum + changeNum);
                    playerResourceBlackNum = PlayerPrefs.GetInt("ResourceBlackNum");
                    break;
                case ResourceType.ResourceWhite:
                    if (!checkResource(r_type, changeNum)) return;
                    PlayerPrefs.SetInt("ResourceWhiteNum", playerResourceWhiteNum + changeNum);
                    playerResourceWhiteNum = PlayerPrefs.GetInt("ResourceWhiteNum");
                    break;
            }
        }
    }
    public bool checkResource(ResourceType type,int changeNum)
    {
        int resourceNum=0;
        switch (type)
        {
            case ResourceType.ResourceGreen:
                resourceNum = playerResourceGreenNum;
                break;
            case ResourceType.ResourceRed:
                resourceNum = playerResourceRedNum;
                break;
            case ResourceType.ResourceBlue:
                resourceNum = playerResourceBlueNum;
                break;
            case ResourceType.ResourceBlack:
                resourceNum = playerResourceBlackNum;
                break;
            case ResourceType.ResourceWhite:
                resourceNum = playerResourceWhiteNum;
                break;
        }
        if (resourceNum + changeNum < 0) return false;
        return true;
    }
    
}
