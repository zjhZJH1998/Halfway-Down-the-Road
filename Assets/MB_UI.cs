using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minion_Data_UI
{
    public string name;
    public string text;
    public int G;
    public int R;
    public int B;
    public Minion_Data_UI(string name, string text,int G,int R,int B)
    {
        this.name = name;
        this.text = text;
        this.G = G;
        this.R = R;
        this.B = B;
    }
}


public class MB_UI : MonoBehaviour
{

    public Button btn;
    public GameObject MB_panel;
    public GameObject Text_panel;

    public List<Minion_Data_UI> MinionList;
    

    private void Awake()
    {
        MinionList = new List<Minion_Data_UI>();
        initMinionInfor();
        btn.onClick.AddListener(onOpenClicked);

        EventCenter.AddListener<int>(EventDefine.onShowDecButtonClicked, onShowDecButtonClicked);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.onShowDecButtonClicked, onShowDecButtonClicked);
    }

    void onOpenClicked()
    {
        if (MB_panel.active == false)
            MB_panel.SetActive(true);
        else MB_panel.SetActive(false) ;
    }

    private void onShowDecButtonClicked(int id)
    {
        Text_panel.transform.Find("Name").GetComponent<Text>().text = MinionList[id].name;
        Text_panel.transform.Find("Dec").GetComponent<Text>().text = MinionList[id].text;
        Text_panel.transform.Find("costNum_G").GetComponent<Text>().text = MinionList[id].G.ToString();
        Text_panel.transform.Find("costNum_R").GetComponent<Text>().text = MinionList[id].R.ToString();
        Text_panel.transform.Find("costNum_B").GetComponent<Text>().text = MinionList[id].B.ToString();
    }

    private void initMinionInfor()
    {
        MinionList.Add(new Minion_Data_UI("composite", "A alchemical creature use a sword to attack",1,1,0));
        MinionList.Add(new Minion_Data_UI("Metal Polymer", "A giant creature made of metal and magic that increases its attack speed with each attack and slows down when it stops",3,2,0));
        MinionList.Add(new Minion_Data_UI("Water Element", "Emit several magic spheres that heal your minions or damage enemies",0,2,4));
        MinionList.Add(new Minion_Data_UI("Flesh Golem", "Unable to attack, but causes reflective damage every time it is attacked.",2,0,5));
        MinionList.Add(new Minion_Data_UI("Spirit Fire", "Moves in a circular pattern around you, damaging any enemies it touches.",2,5,0));
        MinionList.Add(new Minion_Data_UI("Flying Skulls", "Attaches to enemies, causing damage, and cannot be targeted or attacked by enemies.",0,3,3));
    }
}
