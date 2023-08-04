using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dec_UI
{
    public string dec;
    public int imageID;

    public Dec_UI(string dec, int imageID)
    {
        this.dec = dec;
        this.imageID = imageID;
    }
}



public class UpgradePanel : MonoBehaviour
{

    public GameObject Panel;
    public Sprite[] upgradeiconList;
    private List<Dec_UI> upgradePanelList;


    public List<Button> selectionBTNs;
    List<upgradeSelection> selectionList;

    private void Awake()
    {
        initDec();
        selectionList = new List<upgradeSelection>();
        EventCenter.AddListener(EventDefine.UpgradePanelOpen, OpenPanel);
        EventCenter.AddListener(EventDefine.CloseUpgradePanel, ClosePanel);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.UpgradePanelOpen, OpenPanel);
    }

    private void InitSelectionBTNs()
    {

        List<int> sampler = new List<int>();
        for(int i =0;i < levelManager._instance.upgradesLeftList.Count;i++)
        {
            sampler.Add(levelManager._instance.upgradesLeftList[i]);
        }
           
        int size = sampler.Count < 3 ? sampler.Count : 3;
        //Debug.Log(size);
        for (int i = 0;i < size; i++)
        {
            int rand = Random.Range(0, sampler.Count);
            selectionBTNs[i].gameObject.SetActive(true);
            selectionBTNs[i].GetComponent<GroupBTN>().type = (upgradeSelection)sampler[rand];
            Dec_UI tmp = upgradePanelList[rand];
            selectionBTNs[i].transform.Find("Text").GetComponentInChildren<Text>().text = tmp.dec;
            Image temp = selectionBTNs[i].transform.Find("Image").GetComponentInChildren<Image>();
            temp.sprite = upgradeiconList[tmp.imageID];
            Debug.Log(tmp.imageID);
            if (tmp.imageID == 0 || tmp.imageID == 5)
            {
                temp.GetComponent<RectTransform>().sizeDelta = new Vector2(300f, 300f);
            }
            else
            {
                temp.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
            }
            sampler.Remove(sampler[rand]);
        }

        for(int i = 2;i >= size;i--)
        {
            selectionBTNs[i].gameObject.SetActive(false);
        }
    }

    private void OpenPanel()
    {
        InitSelectionBTNs();
        Panel.SetActive(true);
    }

    private void ClosePanel()
    {
        if(Panel != null)
        Panel.SetActive(false);
    }    

    private void initDec()
    {
        upgradePanelList = new List<Dec_UI>();
        upgradePanelList.Add(new Dec_UI("Your composite gain ATK + 1 and HP +1",0));
        upgradePanelList.Add(new Dec_UI("You summon 2 composite once", 0));
        upgradePanelList.Add(new Dec_UI("Metal Polymer will explode upon death.", 1));
        upgradePanelList.Add(new Dec_UI("Your water elemental gains +2 health, and restores health with each attack.", 2));
        upgradePanelList.Add(new Dec_UI("Spirit Fire will slowly regenerate health over time. ", 3));
        upgradePanelList.Add(new Dec_UI("Flesh Golem summons two smaller clones upon death.", 4));
        upgradePanelList.Add(new Dec_UI("Summon 1 extra Flying Skulls, and slows down enemies they attack.", 5));
    }
}
