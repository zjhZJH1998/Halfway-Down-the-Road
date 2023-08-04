using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Town : MonoBehaviour, IPointerClickHandler
{
    public bool isHere;
    public int townID;
    [SerializeField]
    private Text taskPanelText;
    [SerializeField]
    private Image taskPanelImage;
    public Sprite redResource;
    public Sprite greenResource;
    public Sprite blueResource;
    private void Awake()
    {
        taskPanelImage = GameObject.Find("Panels").transform.Find("Task Goal").Find("Canvas").Find("TextFrame").Find("Image").gameObject.GetComponent<Image>();
        taskPanelText = GameObject.Find("Panels").transform.Find("Task Goal").Find("Canvas").Find("TextFrame").Find("TaskText").gameObject.GetComponent<Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isHere)
        {
            //EventCenter.Broadcast(EventDefine.TownPanel);
        }
            
        else
        {
            EventCenter.Broadcast(EventDefine.TaskPanel,this.transform);
            switch (this.transform.name)
            {
                case "TownA":
                    taskPanelText.text = "Go to Town A, you can get:\n25";
                    taskPanelImage.sprite = greenResource;
                    break;
                case "TownB":
                    taskPanelText.text = "Go to Town B, you can get:\n25";
                    taskPanelImage.sprite = redResource;
                    break;
                case "TownC":
                    taskPanelText.text = "Go to Town C, you can get:\n25";
                    taskPanelImage.sprite = blueResource;
                    break;

            }
        }

    }


}
